using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteVisitka.Models.SQL_models.Works
{
    public class Work
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool Prestige { get; set; } = false;
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
