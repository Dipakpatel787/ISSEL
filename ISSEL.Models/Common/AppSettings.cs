using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSEL.Models.Common
{
    public class AppSettings
    {
        public string JWTTokenGenKey { get; set; }
        public string WebAppBaseUrl { get; set; }
        public EmailServiceOptions EmailServiceOptions { get; set; }

    }
}
