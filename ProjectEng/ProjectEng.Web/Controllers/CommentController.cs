using ProjectEng.Models;
using ProjectEng.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEng.Web.Controllers
{
    public class CommentController : Controller
    {
        private IRepositoryTask _repositoryTask;
        // GET: Comment
        public ActionResult CommentList()
        {
            return View();
        }

        public ActionResult CreateComment()
        {
            _repositoryTask = new RepositoryTask();
            var listTask = _repositoryTask.GetListTask();
            ViewBag.TaskID = new SelectList(listTask, "ID", "Description");
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            return View();
        }
    }
}