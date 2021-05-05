using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteVisitka.Models;
using SiteVisitka.Models.SQL_models.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private WorksContext db;
        public WorkController(WorksContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> Get()
        {
            return await db.Works.Include(x => x.Images).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Work>> Post(ArgumentClass arguments)
        {
            Work work = new Work()
            {
                Name = arguments.Name,
                Description = arguments.Description,
                Address = arguments.Address
            };

            if (arguments.urlImages == null)
            {
                return NoContent();
            }

            List<Image> images = new List<Image>();
            foreach (string url in arguments.urlImages.Split('\n'))
            {
                images.Add(new Image() { url = url });
            }
            work.Images.AddRange(images);

            db.Images.AddRange(images);
            db.Works.Add(work);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
