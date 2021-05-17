using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteVisitka.Models;
using SiteVisitka.Models.SQL_models.Works;
using SiteVisitka.Serviсes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteVisitka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private WorksContext db;
        private readonly ManagerLoginAdmin _managerLoginAdmin;
        private readonly MyFileLogger _logger;

        public WorkController(WorksContext context, ManagerLoginAdmin managerLoginAdmin, MyFileLogger logger)
        {
            db = context;
            _managerLoginAdmin = managerLoginAdmin;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> Get()
        {
            try
            {
                return await db.Works.Include(x => x.Images).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }


        [HttpPost]
        public ActionResult Post(ArgumentClass arguments)
        {
            if (!_managerLoginAdmin.IsStatusOK(HttpContext))
                return Forbid();

            if (string.IsNullOrWhiteSpace(arguments.urlImages))
                ModelState.AddModelError("urls", "пустое поле");

            if (string.IsNullOrWhiteSpace(arguments.Name))
                ModelState.AddModelError("name", "пустое поле");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Work work = new Work()
            {
                Name = arguments.Name,
                Description = arguments.Description,
                Address = arguments.Address,
                Prestige = arguments.Prestige
            };

            List<Image> images = new();
            foreach (string url in arguments.urlImages.Split('\n'))
            {
                images.Add(new Image() { url = url });
            }
            work.Images.AddRange(images);

            db.Images.AddRange(images);
            db.Works.Add(work);

            try
            {
                db.SaveChanges();
                _logger.logAddWorkToDB(work);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return BadRequest(ex);
            }
        }

        [HttpPut]
        public ActionResult Put(ArgumentClass arguments)
        {
            string request = arguments.SqlRequest;
            try
            {
                db.Database.ExecuteSqlRaw(request);
                _logger.logPostSqlRequest(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return BadRequest();
            }
        }
    }
}
