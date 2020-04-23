using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Shared.Context
{
    public static class ModelBuilderExtesions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        { 
            //modelBuilder.Entity<UserModel>().HasData(
            //    new UserModel()
            //    {
            //        Id = Guid.NewGuid(),
            //        FirstName = "admin",
            //        LastName = "admin",
            //        Email = "admin@gmail.com",
            //        DOB = DateTime.Now,
            //        Password = "Admin@123"
            //    });
        }
    }
}
