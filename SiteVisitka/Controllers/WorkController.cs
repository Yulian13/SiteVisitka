using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SiteVisitka.Models;
using SiteVisitka.Models.SQL_models.Works;
using SiteVisitka.Serviсes;
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
        private readonly ManagerLoginAdmin _managerLoginAdmin;

        public WorkController(WorksContext context, ManagerLoginAdmin managerLoginAdmin)
        {
            db = context;
            _managerLoginAdmin = managerLoginAdmin;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> Get()
        {
            return await db.Works.Include(x => x.Images).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Work>> Post(ArgumentClass arguments)
        {
            if (!_managerLoginAdmin.IsStatusOK(HttpContext))
                ModelState.AddModelError("pass","Неверный пароль");

            if (string.IsNullOrWhiteSpace(arguments.urlImages))
                ModelState.AddModelError("urls", "пустое поле");
            if (string.IsNullOrWhiteSpace(arguments.Name))
                ModelState.AddModelError("name", "пустое поле");
            if (string.IsNullOrWhiteSpace(arguments.Description))
                ModelState.AddModelError("description", "пустое поле");
            if (string.IsNullOrWhiteSpace(arguments.Address))
                ModelState.AddModelError("address", "пустое поле");

            if (!ModelState.IsValid)
                return BadRequest();

            Work work = new Work()
            {
                Name = arguments.Name,
                Description = arguments.Description,
                Address = arguments.Address
            };

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
