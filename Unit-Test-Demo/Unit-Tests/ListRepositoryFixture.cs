using Unit_Tests;
using Unit_Test_Demo.DAL;

namespace Unit.Tests
{
    public class ListRepositoryFixture
    {
        public IContainerRepository Repo { get; private set; }

        public ListRepositoryFixture()
        {
            Repo = new TestContainerRepository();
        }
    }
}
