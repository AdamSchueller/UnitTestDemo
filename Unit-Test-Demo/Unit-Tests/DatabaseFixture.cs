using Microsoft.EntityFrameworkCore;
using Unit_Test_Demo.DAL;
using Unit_Test_Demo.Domain;

namespace Unit.Tests
{
    public class DatabaseFixture
    {
        public DemoContext Context { get; private set; }

        public DatabaseFixture()
        {
            var connectionString = "Server=localhost;Database=UnitTestDemo;Trusted_Connection=True";
            var builder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            Context = new DemoContext(builder.Options);

            Context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Containers]");
        }
    }
}
