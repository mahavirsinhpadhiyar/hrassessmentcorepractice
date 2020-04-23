using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public class ConsultantModel : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
