using System;
using Unit_Test_Demo.Domain;
using Xunit;

namespace Unit.Tests
{
    public class ContainerMaxCapacityTests
    {
        [Fact]
        public void Can_create_container_when_max_capacity_valid()
        {
            //Arrange & Act
            var newContainer = new Container(1);

            //Assert
            Assert.NotNull(newContainer);
            Assert.Equal(newContainer.MaxCapacity, 1);
        }

        [Fact]
        public void Can_not_create_container_when_max_capacity_invalid()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Container(-1));
        }

        [Fact]
        public void Can_pack_when_current_capacity_less_than_max_capacity()
        {
            //Arrange
            var container = new Container(2);
            container.UpdateCurrentCapacity(1);

            //Act
            container.Pack();

            //Assert
            Assert.Equal(container.CurrentCapacity, 2);
        }

        [Fact]
        public void Can_pack_when_max_capacity_is_zero()
        {
            //Arrange
            var container = new Container(0);
            container.UpdateCurrentCapacity(999999);

            //Act
            container.Pack();

            //Assert
            Assert.Equal(container.CurrentCapacity, 1000000);
        }

        [Fact]
        public void Can_not_pack_when_current_capacity_equal_to_max_capacity()
        {
            //Arrange
            var container = new Container(3);
            container.UpdateCurrentCapacity(3);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => container.Pack());
        }

        [Fact]
        public void Can_not_pack_when_current_capacity_greater_than_max_capacity()
        {
            //Arrange
            var container = new Container(3);
            container.UpdateCurrentCapacity(4);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => container.Pack());
        }
    }
}
