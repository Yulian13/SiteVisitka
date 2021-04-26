using Microsoft.AspNetCore.Mvc;
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
        }

        public IActionResult Main()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
