using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.Models.SQL_models.Works
{
    public class Image
    {
        public int id { get; set; }
        public string url { get; set; }
        public int WotkID { get; set; }
        public Work Work { get; set; }
    }
}
