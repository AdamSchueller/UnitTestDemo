using Microsoft.EntityFrameworkCore;
using Unit_Test_Demo.Domain;

namespace Unit_Test_Demo.DAL
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //Entity DbSets
        public virtual DbSet<Container> Containers { get; set; }
    }
}
