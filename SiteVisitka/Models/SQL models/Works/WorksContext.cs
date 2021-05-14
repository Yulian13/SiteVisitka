using Microsoft.EntityFrameworkCore;
using SiteVisitka.Models.SQL_models.Comments;

namespace SiteVisitka.Models.SQL_models.Works
{
    public class WorksContext : DbContext
    {
        public DbSet<Work> Works { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public WorksContext(DbContextOptions<WorksContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
