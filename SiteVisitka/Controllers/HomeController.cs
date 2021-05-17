using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SiteVisitka.Models;
using SiteVisitka.Models.SQL_models.Works;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SiteVisitka.Controllers
{
    public class HomeController : Controller
    {
        private readonly WorksContext db;

        public HomeController(WorksContext context, IConfiguration configuration)
        {
            db = context;

            db.Images.Load();
            if (!db.Works.Any())
            {
                Work work = new Work() { Name = "test", Description = "Description", Address = "Address", Prestige = true };

                List<Image> images = new List<Image>() {
                    new Image() { url = configuration["Urls:1"], Work = work},
                    new Image() { url = configuration["Urls:2"], Work = work},
                    new Image() { url = configuration["Urls:3"], Work = work},
                    new Image() { url = configuration["Urls:4"], Work = work}
                };

                work.Images.AddRange(images);

                db.Works.Add(work);
                db.Images.AddRange(images);
                db.SaveChanges();
            }
        }

        public IActionResult Main()
        {
            return View(db);
        }

        [HttpGet]
        public IActionResult Works()
        {
            return View(db.Works.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
