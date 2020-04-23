using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public ICollection<ConsultantModel> Consultants { get; set; }
        public ICollection<CompanyModel> Companys { get; set; }
    }
}
