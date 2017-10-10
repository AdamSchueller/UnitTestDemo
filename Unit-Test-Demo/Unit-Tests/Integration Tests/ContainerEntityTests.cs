using Microsoft.EntityFrameworkCore;
using Unit_Test_Demo.DAL;
using Unit_Test_Demo.Domain;
using Xunit;

namespace Unit.Tests
{
    public class ContainerEntityTests : IClassFixture<DatabaseFixture>
    {
        private readonly DemoContext _context;

        public ContainerEntityTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void Can_write_container()
        {
            //Arrange
            var newContainer = new Container
            {
                CurrentCapacity = 1,
                MaxCapacity = 2
            };

            //Act
            var ex = Record.Exception(() =>
            {
                _context.Add(newContainer);
                _context.SaveChanges();
            });

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void Can_read_container()
        {
            //Arrange
            int id = _context.Database.ExecuteSqlCommand($"INSERT INTO Containers (MaxCapacity, CurrentCapacity) VALUES (50, 100)");

            //Act
            Container container = null;
            var ex = Record.Exception(() =>
            {
                container = _context.Containers.Find(id);
            });

            //Assert
            Assert.Null(ex);
            Assert.NotNull(container);
        }
    }
}
