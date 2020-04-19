using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssessmentAPI.Helpers
{
    /// <summary>
    /// Appsettings properties use to access across the application
    /// by definfing inside DP.
    /// </summary>
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
