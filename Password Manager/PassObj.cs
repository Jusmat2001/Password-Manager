using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    public class PassObj
    {
        public string siteName { get; set; }
        public string siteUrl { get; set; }
        public string siteId { get; set; }
        public string sitePass { get; set; }
        public DateTime lastChanged { get; set; }

    }
}
