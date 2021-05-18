using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteVisitka.Models;
using SiteVisitka.Models.SQL_models.Comments;
using SiteVisitka.Models.SQL_models.Works;
using SiteVisitka.Serviсes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiteVisitka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private WorksContext db;
        private readonly ManagerLoginAdmin _managerLoginAdmin;
        private readonly MyFileLogger _logger;

        public CommentsController(WorksContext context, ManagerLoginAdmin managerLoginAdmin, MyFileLogger logger)
        {
            db = context;
            _managerLoginAdmin = managerLoginAdmin;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            try
            {
                return db.Comments.Where(x => x.Approved).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> Get(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment?.Approved ?? false || _managerLoginAdmin.IsStatusOK(HttpContext))
                return comment;
            else
                return Forbid();
        }

        [HttpPost]
        public ActionResult Post([FromBody] Comment comment)
        {
            if (!_managerLoginAdmin.IsStatusOK(HttpContext))
                return Forbid();

            if (string.IsNullOrWhiteSpace(comment.Name))
                ModelState.AddModelError("name", "пустое поле имени");

            if (string.IsNullOrWhiteSpace(comment.Text))
                ModelState.AddModelError("text", "пустое поле комментария");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Comments.Add(comment);

            try
            {
                db.SaveChanges();
                _logger.LogAddCommentToDB(comment);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] bool isOK)
        {
            if (!_managerLoginAdmin.IsStatusOK(HttpContext))
                return Forbid();

            Comment comment = db.Comments.Find(id);
            if (comment == null)
                return NotFound();

            comment.Approved = isOK;
            try
            {
                db.SaveChanges();
                _logger.LogAddCommentToDB(comment);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return BadRequest(ex);
            }
        }
    }
}
