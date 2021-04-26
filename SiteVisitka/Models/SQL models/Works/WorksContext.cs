using Microsoft.EntityFrameworkCore;

namespace SiteVisitka.Models.SQL_models.Works
{
    public class WorksContext : DbContext
    {
        public DbSet<Work> Works { get; set; }
        public DbSet<Image> Images { get; set; }

        public WorksContext(DbContextOptions<WorksContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
