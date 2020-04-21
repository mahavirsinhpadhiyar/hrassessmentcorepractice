using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
    public class CompanyVM
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
    }
}
