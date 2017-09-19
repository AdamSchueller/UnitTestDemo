using System;
using Unit_Test_Demo.Controllers;
using Unit_Test_Demo.DAL;
using Unit_Test_Demo.Domain;
using Xunit;

namespace Unit.Tests
{
    public class ContainerMaxCapacityTests : IClassFixture<DatabaseFixture>
    {
        private readonly DemoContext _context;
        private readonly ContainersController _containerController;

        public ContainerMaxCapacityTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;
            _containerController = new ContainersController(fixture.Context);
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

            //Assert
            Assert.Equal(container.CurrentCapacity, 2);
        }

        [Fact]
        public void Can_not_pack_when_current_capacity_greater_than_or_equal_to_max_capacity()
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
            Assert.Throws<Exception>(() => _containerController.PackItemIntoContainer(container.Id));

        }
    }
}
