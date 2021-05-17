using System.ComponentModel.DataAnnotations;

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
