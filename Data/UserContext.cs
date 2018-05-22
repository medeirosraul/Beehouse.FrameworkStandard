using Beehouse.FrameworkStandard.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beehouse.FrameworkStandard.Data
{
    public class UserContext: StandardContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            // -- Constructor
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<UserEntity>();
        }
    }
}