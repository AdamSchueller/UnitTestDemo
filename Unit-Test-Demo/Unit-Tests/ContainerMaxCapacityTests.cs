using System;
using Unit_Test_Demo.Commands;
using Unit_Test_Demo.Controllers;
using Unit_Test_Demo.DAL;
using Unit_Test_Demo.Domain;
using Xunit;

namespace Unit.Tests
{
    public class ContainerMaxCapacityTests : IClassFixture<InMemoryDatabaseFixture>
    {
        private readonly DemoContext _context;
        private readonly ContainersController _containerController;

        public ContainerMaxCapacityTests(InMemoryDatabaseFixture fixture)
        {
            _context = fixture.Context;
            _containerController = new ContainersController(fixture.Context);
        }

        [Fact]
        public void Can_create_container_when_max_capacity_valid()
        {
            //Arrange
            var createCommand = new CreateContainerCommand
            {
                MaxCapacity = 1
            };

            //Act
            var newContainerId = _containerController.CreateContainer(createCommand);
            var newContainer = _containerController.GetContainerById(newContainerId);

            //Assert
            Assert.NotNull(newContainer);
            Assert.Equal(newContainer.MaxCapacity, 1);
        }

        [Fact]
        public void Can_not_create_container_when_max_capacity_invalid()
        {
            //Arrange
            var createCommand = new CreateContainerCommand
            {
                MaxCapacity = -1
            };

            //Act & Assert
            Assert.Throws<ArgumentException>(() => _containerController.CreateContainer(createCommand));
        }

        [Fact]
        public void Can_pack_when_current_capacity_less_than_max_capacity()
        { 
            //Arrange
            var container = new Container
            {
                CurrentCapacity = 1,
                MaxCapacity = 2
            };
            _context.Containers.Add(container);
            _context.SaveChanges();
        
            //Act
            _containerController.PackItemIntoContainer(container.Id);
            container = _containerController.GetContainerById(container.Id); //read back

            //Assert
            Assert.Equal(container.CurrentCapacity, 2);
        }

        [Fact]
        public void Can_pack_when_max_capacity_is_zero()
        {
            //Arrange
            var container = new Container
            {
                CurrentCapacity = 999999,
                MaxCapacity = 0
            };
            _context.Containers.Add(container);
            _context.SaveChanges();

            //Act
            _containerController.PackItemIntoContainer(container.Id);
            container = _containerController.GetContainerById(container.Id); //read back

            //Assert
            Assert.Equal(container.CurrentCapacity, 1000000);
        }

        [Fact]
        public void Can_not_pack_when_current_capacity_equal_to_max_capacity()
        {
            //Arrange
            var container = new Container
            {
                CurrentCapacity = 3,
                MaxCapacity = 3
            };
            _context.Containers.Add(container);
            _context.SaveChanges();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => _containerController.PackItemIntoContainer(container.Id));
        }

        [Fact]
        public void Can_not_pack_when_current_capacity_greater_than_max_capacity()
        {
            //Arrange
            var container = new Container
            {
                CurrentCapacity = 4,
                MaxCapacity = 3
            };
            _context.Containers.Add(container);
            _context.SaveChanges();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => _containerController.PackItemIntoContainer(container.Id));
        }
    }
}
