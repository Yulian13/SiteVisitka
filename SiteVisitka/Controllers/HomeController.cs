using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SiteVisitka.Models;
using SiteVisitka.Models.SQL_models.Works;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.Controllers
{
    public class HomeController : Controller
    {
        private readonly WorksContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, WorksContext context)
        {
            _logger = logger;
            db = context;
            db.Images.Load();
            if (!db.Works.Any())
            {
                Work work = new Work() { Name="test", Description = "Description", Address= "Address" };

                List<Image> images = new List<Image>() { 
                    new Image() { url = @"https://sun9-68.userapi.com/impf/H4PYyEU4KKSAdgIArrLgIMO4iulOTMiUG2yH_A/eh6Td8mnviI.jpg?size=1037x584&quality=96&sign=486e9a9394ee484cea852dd373d8cb47&type=album", Work = work},
                    new Image() { url = @"https://sun9-18.userapi.com/impf/psPFReFlbichEasBeU3OJ89k37b0SAOfLLHc7A/wOLpwr87POw.jpg?size=1037x584&quality=96&sign=32c97eedafacd598072d3d21439d04be&type=album", Work = work},
                    new Image() { url = @"https://sun9-16.userapi.com/impf/MwcPfoObWEfsstYaEE8Gn9ZcTCMRCAzSL0Wl6Q/2PLzERTD3QU.jpg?size=1037x584&quality=96&sign=420a7fe6d0c140301e4ea73f2348dc3f&type=album", Work = work},
                    new Image() { url = @"https://sun9-19.userapi.com/impf/gD8_ag0_cBrUV-I5DQU_f51oZBtjUD01OhpZOg/4K4nldnn1U4.jpg?size=1037x584&quality=96&sign=4852f522aa816f7220afa28cf3a590d3&type=album", Work = work}
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
