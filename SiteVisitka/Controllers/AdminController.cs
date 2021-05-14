using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteVisitka.Models.SQL_models.Works;
using SiteVisitka.Serviсes;
using System.Collections.Generic;
using System.Linq;

namespace SiteVisitka.Controllers
{
    public class AdminController : Controller
    {
        private readonly WorksContext db;
        private readonly ManagerLoginAdmin _managerLoginAdmin;

        public AdminController(WorksContext context, ManagerLoginAdmin managerLoginAdmin)
        {
            db = context;
            _managerLoginAdmin = managerLoginAdmin;

            db.Images.Load();
        }

        [HttpGet]
        public ActionResult InputPass([FromQuery] string action = "Main")
        {
            ViewData["act"] = action;
            return View("inputPass");
        }
        [HttpPost]
        public object InputPass(string pass, string action)
        {
            if (_managerLoginAdmin.SetStatusAndCheckPassword(pass, HttpContext))
            {
                return View(action, db);
            }
            else
                return "Invalid password";
        }

        public IActionResult Main()
        {
            return View(db);
        }

        public IActionResult AddWork()
        {
            return View();
        }

        public IActionResult PutSqlRequest()
        {
            return View();
        }
    }
}
