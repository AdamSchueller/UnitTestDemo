using System.Collections.Generic;
using Unit_Test_Demo.Domain;

namespace Unit_Test_Demo.DAL
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly DemoContext _context;

        public ContainerRepository(DemoContext context)
        {
            _context = context;
        }

        public IEnumerable<Container> FindAllContainers()
        {
            return _context.Containers;
        }

        public Container FindContainerById(int id)
        {
            return _context.Containers.Find(id);
        }

        public int AddContainer(Container container)
        {
            _context.Containers.Add(container);
            _context.SaveChanges();
            return container.Id;
        }

        public void UpdateContainer(Container container)
        {
            _context.Containers.Attach(container);
            _context.SaveChanges();
        }
    }
}
