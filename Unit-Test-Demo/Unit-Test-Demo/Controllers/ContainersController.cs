using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Unit_Test_Demo.Commands;
using Unit_Test_Demo.DAL;
using Unit_Test_Demo.Domain;

namespace Unit_Test_Demo.Controllers
{
    [Route("api/[controller]")]
    public class ContainersController : Controller
    {
        private readonly IContainerRepository _repo;

        public ContainersController(IContainerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public List<Container> GetAllContainers()
        {
            return _repo.FindAllContainers().ToList();
        }

        [HttpGet("{id}")]
        public Container GetContainerById(int id)
        {
            return _repo.FindContainerById(id);
        }

        [HttpPost]
        public int CreateContainer([FromBody]CreateContainerCommand command)
        {
            if (command.MaxCapacity < 0)
            {
                throw new ArgumentException("Create container failed: max capacity must be a positive integer!");
            }

            var newContainer = new Container
            {
                MaxCapacity = command.MaxCapacity
            };

            _repo.AddContainer(newContainer);

            return newContainer.Id;
        }

        [HttpPost("{id}/pack")]
        public void PackItemIntoContainer(int id)
        {
            var container = _repo.FindContainerById(id);

            if (container == null)
            {
                throw new ObjectNotFoundException($"Pack failed: container with ID [{id}] not found!");
            }

            if (container.MaxCapacity == 0 || container.CurrentCapacity < container.MaxCapacity)
            {
                container.CurrentCapacity++;
                _repo.UpdateContainer(container);
            }
            else
            {
                throw new InvalidOperationException("Pack failed: container is already at maximum capacity!");
            }
        }   
    }
}
