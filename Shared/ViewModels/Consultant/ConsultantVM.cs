    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.ViewModels.Consultant
{
    public class ConsultantVM
    {
        public ConsultantVM()
        {
            CompanyList = new List<CompanyVM>();
        }
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Is Admin")]
        public bool IsAdmin { get; set; }
        [Required(ErrorMessage = "Company is required")]
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UserId { get; set; }
        public List<CompanyVM> CompanyList { get; set; }
    }
}
