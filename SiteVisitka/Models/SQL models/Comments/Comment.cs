using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.Models.SQL_models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string Text { get; set; }
        public bool Approved { get; set; } = false;
    }
}
