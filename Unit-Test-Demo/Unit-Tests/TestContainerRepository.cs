using System.Collections.Generic;
using System.Linq;
using Unit_Test_Demo.DAL;
using Unit_Test_Demo.Domain;

namespace Unit_Tests
{
    class TestContainerRepository : IContainerRepository
    {
        private readonly List<Container> _containers;

        public TestContainerRepository()
        {
            _containers = new List<Container>();
        }

        public IEnumerable<Container> FindAllContainers()
        {
            return _containers;
        }

        public Container FindContainerById(int id)
        {
            return _containers.FirstOrDefault(c => c.Id == id);
        }

        public int AddContainer(Container container)
        {
            container.Id = _containers.Count - 1;
            _containers.Add(container);
            return container.Id;
        }

        public void UpdateContainer(Container container)
        {
            var index = _containers.FindIndex(c => c.Id == container.Id);
            _containers[index] = container;
        }
    }
}
