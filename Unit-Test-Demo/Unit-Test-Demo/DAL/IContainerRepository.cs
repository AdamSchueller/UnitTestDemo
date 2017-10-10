using System.Collections.Generic;
using Unit_Test_Demo.Domain;

namespace Unit_Test_Demo.DAL
{
    public interface IContainerRepository
    {
        IEnumerable<Container> FindAllContainers();
        Container FindContainerById(int id);
        int AddContainer(Container container);
        void UpdateContainer(Container container);
    }
}