using Beehouse.FrameworkStandard.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beehouse.FrameworkStandard.Data
{
    public class StandardContext:DbContext
    {
        public StandardContext(DbContextOptions<StandardContext> options) : base(options)
        {
            // -- Constructor
        }

        protected StandardContext(DbContextOptions options) : base(options)
        {
            // -- Constructor
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Entity>();
        }
    }
}