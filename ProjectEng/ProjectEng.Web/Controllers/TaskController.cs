using ProjectEng.Models;
using ProjectEng.Repositories.Repository;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEng.Web.Controllers
{
    
    public class TaskController : Controller
    {
        private IRepositoryTask _repositorytask;
        private IRepositoryProject _repositoryproject;
        private IRepositoryStaff _repositoryStaff;
        // GET: Task
        public ActionResult Index(string pMStaff,int id = 0)
        {
            _repositorytask = new RepositoryTask();
            var listtak = _repositorytask.GetListTaskByProjectId_All(id);
            ViewBag.PMStaff = pMStaff;
            return View(listtak);
        }

        public ActionResult CreateTask(string ProjectId, string pMStaff)
        {          
            ViewBag.ProjectId = ProjectId;
            ViewBag.PM = pMStaff;
            loadPMStaff("ALL");

            return View();
        }

        [HttpPost]
        public ActionResult CreateTask(ProjectEng.Models.Task task, string pMStaff)
        {
            _repositorytask = new RepositoryTask();
            _repositoryproject = new RepositoryProject();

            if(ModelState.IsValid){
                task.Create = DateTime.Now;
                OperationStatus ops = _repositorytask.SaveTask(task);
                if (!ops.Status)
                {
                    ModelState.AddModelError("", ops.ExceptionInnerMessage);
                }
                else
                {
                    ModelState.AddModelError("", "Data Save!!");
                }
            }

            ViewBag.ProjectId = task.ProjectID;
            ViewBag.PMStaff = pMStaff;
            loadPMStaff("ALL");

            return RedirectToAction("CreateTask", new { ProjectId = task.ProjectID, PMStaff = pMStaff });
        }

        public ActionResult Details(int id)
        {
            _repositorytask = new RepositoryTask();
            var list = _repositorytask.GetListTaskByProjectId(id);
            return PartialView(list);
        }

        public ActionResult Edit(int id,string pMStaff)
        {
            _repositorytask = new RepositoryTask();
            var task = _repositorytask.GetTaskByID(id);

            //ViewBag.ProjectID = task.ProjectID;
            loadPMStaff(task.Responsable);
            ViewBag.PMStaff = pMStaff;
            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(ProjectEng.Models.Task task, string pMStaff)
        {
            _repositorytask = new RepositoryTask();
            _repositoryproject = new RepositoryProject();

            if (ModelState.IsValid)
            {
                //task.Create = DateTime.Now;
                OperationStatus ops = _repositorytask.SaveTask(task);
                if (!ops.Status)
                {
                    ModelState.AddModelError("", ops.ExceptionInnerMessage);
                }
                else
                {
                    ModelState.AddModelError("", "Data Save!!");
                }


            }
            var listproject = _repositoryproject.GetListProject("");
            ViewBag.ProjectId = new SelectList(listproject, "ProjectId", "Description");

            ViewBag.ProjectId = task.ProjectID;
            ViewBag.PMStaff = pMStaff;
            loadPMStaff("ALL");
            return RedirectToAction("ProjectList", "Project", new { ProjectId = task.ProjectID, PMStaff = pMStaff });
        }
        public JsonResult AddStaffAsync(string staffname, string staffdescription)
        {
            _repositoryStaff = new RepositoryStaff();
            Staff addstaff = new Staff();
            addstaff.StaffName = staffname;
            addstaff.StaffPosition = staffdescription;

            OperationStatus ops = _repositoryStaff.saveStaff(addstaff);
            if (ops.Status)
            {
                return Json(new { StaffId = addstaff.StaffId, StaffName = addstaff.StaffName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ArgumentException(ops.ExceptionInnerMessage), JsonRequestBehavior.AllowGet);
            }
           
        }


        public ActionResult TaskToPdf(int id,string page)
        {
            _repositorytask = new RepositoryTask();

            //int idp = int.Parse(page);
            var model = _repositorytask.GetListTaskByProjectId_All(id);

            return new PartialViewAsPdf("_TaskProject", model)
            {
                FileName = "_TaskProject.pdf",
                PageSize = Size.Letter,
                PageOrientation = Orientation.Portrait,
                MinimumFontSize = 14,
                PageMargins = { Top = 20, Bottom = 10 },
                PageHeight = 40,
                CustomSwitches =
                    "--footer-center \"Name: " + "_TaskProject" + "  DOS: " +
                    DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };
        }

        #region Private method
        private void loadPMStaff(string staffname)
        {
            _repositoryStaff = new RepositoryStaff();
            var liststaff = _repositoryStaff.GetListStaff();

            ViewBag.Responsable = new SelectList(liststaff, "StaffName", "StaffName", staffname);
           
        }


        #endregion
    }
}