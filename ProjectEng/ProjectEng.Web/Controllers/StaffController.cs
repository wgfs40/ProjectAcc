using ProjectEng.Models;
using ProjectEng.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEng.Web.Controllers
{
    
    public class StaffController : Controller
    {
        private IRepositoryStaff _repositoryStaff;

        // GET: Staff
        public ActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStaff(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _repositoryStaff = new RepositoryStaff();
                var ops = _repositoryStaff.saveStaff(staff);
                if (ops.Status)
                {
                    return RedirectToAction("ProjectList", "Project");
                }
                else
                {
                    ModelState.AddModelError("",ops.ExceptionInnerMessage);
                }
            }
            return View();
        }
    }
}