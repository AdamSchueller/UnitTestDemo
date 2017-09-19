using System;
using System.Collections.Generic;
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
        private readonly DemoContext _context;

        public ContainersController(DemoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Container> GetAllContainers()
        {
            return _context.Containers.ToList();
        }

        [HttpGet("{id}")]
        public Container GetContainerById(int id)
        {
            return _context.Containers.FirstOrDefault(c => c.Id == id);
        }

        [HttpPost]
        public void CreateContainer([FromBody]CreateContainerCommand command)
        {
            var newContainer = new Container
            {
                MaxCapacity = command.MaxCapacity
            };

            _context.Containers.Add(newContainer);
            _context.SaveChanges();
        }

        [HttpPost("{id}/pack")]
        public void PackItemIntoContainer(int id)
        {
            var container = _context.Containers.FirstOrDefault(c => c.Id == id);

            if (container.CurrentCapacity < container.MaxCapacity)
            {
                container.CurrentCapacity++;
            }
            else
            {
                throw new Exception("Pack failed: container is already at maximum capacity!");
            }
        }   
    }
}
