using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels.Consultant
{
    public class ConsultantResultSet
    {
        public List<ConsultantVM> ConsultantList { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
    }
}
