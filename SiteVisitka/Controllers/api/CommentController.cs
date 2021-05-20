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

namespace SiteVisitka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private WorksContext db;
        private readonly ManagerLoginAdmin _managerLoginAdmin;
        private readonly MyFileLogger _logger;

        public CommentController(WorksContext context, ManagerLoginAdmin managerLoginAdmin, MyFileLogger logger)
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
                bool isOkStatus = _managerLoginAdmin.IsStatusOK(HttpContext);
                return db.Comments.Where(x => (isOkStatus | x.Approved)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return new List<Comment>() { new Comment() { Name="Error" } };
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> Get(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
                return NotFound();

            if (comment.Approved || _managerLoginAdmin.IsStatusOK(HttpContext))
                return comment;
            else
                return Forbid();
        }

        [HttpPost]
        public ActionResult Post([FromBody] Comment comment)
        {
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

                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] Comment com)
        {
            if (!_managerLoginAdmin.IsStatusOK(HttpContext))
                return Forbid();

            Comment comment = db.Comments.Find(com.Id);
            if (comment == null)
                return NotFound();

            comment.Approved = true;
            try
            {
                db.SaveChanges();
                _logger.LogAddCommentToDB(comment);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!_managerLoginAdmin.IsStatusOK(HttpContext))
                return Forbid();

            Comment comment = db.Comments.Find(id);
            if (comment == null)
                return NotFound();

            db.Comments.Remove(comment);
            return Ok();
        }
    }
}
