using Microsoft.EntityFrameworkCore;
using Unit_Test_Demo.DAL;

namespace Unit.Tests
{
    public class InMemoryDatabaseFixture
    {
        public DemoContext Context { get; private set; }

        public InMemoryDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            Context = new DemoContext(options);
        }
    }
}
