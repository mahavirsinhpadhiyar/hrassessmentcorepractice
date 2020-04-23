using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Entities
{
    public class CompanyModel : BaseEntity
    {
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<ConsultantModel> Consultants { get; set; }
    }
}
