using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Shared.Context
{
    public class HRADbContext : IdentityDbContext<AppUser>
    {
        public HRADbContext(DbContextOptions<HRADbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<ConsultantModel> Consultants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Seed();
        }
    }
}
