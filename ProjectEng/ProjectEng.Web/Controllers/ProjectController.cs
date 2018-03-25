using ProjectEng.Models;
using ProjectEng.Repositories.Repository;
using ProjectEng.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace ProjectEng.Web.Controllers
{
    
    public class ProjectController : Controller
    {
        private IRepositoryProject _repositoryProject;
        private IRepositoryCustomer _repositoryCustomer;
        private IRepositoryStaff _repositoryStaff;
        private const int pagedcount = 10;
        // GET: Project
        public ActionResult ProjectList(string sortparam,string PMStaff, string Status, string customeridpage, string pMStaffpage, string statuspage, int? page, int customerid = -1)
        {
            _repositoryProject = new RepositoryProject();
            _repositoryCustomer = new RepositoryCustomer();

            loadCustomer();
            loadPMStaff(pMStaffpage);

            ViewBag.Customeridpage = customeridpage ?? customerid.ToString();
            ViewBag.PMStaffpage = pMStaffpage ?? PMStaff;

            var statuspaged = Status ?? "A";
            ViewBag.Statuspage = statuspage ?? statuspaged;
            ViewBag.CustomerName = string.IsNullOrEmpty(sortparam) ? "CustomerName" : "";
            ViewBag.CurrentFilter = sortparam;
            string sort = sortparam;

            var pagednumber = page ?? 1;
            if (string.IsNullOrEmpty(PMStaff) && string.IsNullOrEmpty(pMStaffpage))
            {
                var list = _repositoryProject.GetListProject(sort, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);

                return View(list);
            }
            else
            {
                if (customerid != -1)
                {
                    if (!PMStaff.Equals("ALL"))
                    {
                        customerid = customerid != -1 ? customerid : int.Parse(customeridpage);
                        PMStaff = pMStaffpage;
                        Status = statuspage;

                        var list = _repositoryProject.GetListProjectByCustomerId(sort,customerid, PMStaff, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);

                        return View(list);
                    }
                    else
                    {
                        customerid = customerid != -1 ? customerid : int.Parse(customeridpage);
                        Status = statuspage;
                        var list = _repositoryProject.GetListProjectByCustomerId(sort,customerid, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);
                        return View(list);
                    }

                }

                PMStaff = pMStaffpage ?? PMStaff;
                if (PMStaff.Equals("ALL"))
                {
                    Status = statuspage;
                    var list = _repositoryProject.GetListProject(sort, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);
                    return View(list);
                }
                else
                {

                    Status = statuspage;
                    var list = _repositoryProject.GetListProjectByPM(sort, PMStaff, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);
                    return View(list);
                }


            }

            
            return View();
        }

        public ActionResult CreateProject()
        {
            _repositoryCustomer = new RepositoryCustomer();
            _repositoryStaff = new RepositoryStaff();
            var listcustomer = _repositoryCustomer.GetListcustomer();
            ViewBag.CustomerId = new SelectList(listcustomer, "CustomerId", "CustomerName");

            loadStaf();

            return View();
        }


        [HttpPost]
        public ActionResult CreateProject(Project project, string CMStaff, string PMStaff, string PDEStaff, string SEStaff, string QEStaff, string DRStaff)
        {
            _repositoryProject = new RepositoryProject();
            _repositoryCustomer = new RepositoryCustomer();
            project.ProjectStaffs = new ProjectStaff();

            //int projectid = project.ProjectID;

            project.ProjectStaffs.CMStaff = CMStaff;
            project.ProjectStaffs.PMStaff = PMStaff;
            project.ProjectStaffs.PDEStaff = PDEStaff;
            project.ProjectStaffs.SEStaff = SEStaff;
            project.ProjectStaffs.QEStaff = QEStaff;
            project.ProjectStaffs.DRStaff = DRStaff;
            //project.ProjectStaffs.ProjectId = projectid;

            project.Create = DateTime.Now;

            var listcustomer = _repositoryCustomer.GetListcustomer();


            if (ModelState.IsValid)
            {
                project.ProjectStatus = Status.Add;
                OperationStatus ops = _repositoryProject.SaveProject(project);
                if (ops.Status)
                {   
                    return RedirectToAction("ProjectList");
                }
                else
                {
                    ModelState.AddModelError("",ops.ExceptionInnerMessage);
                }
            }

            _repositoryStaff = new RepositoryStaff();
            loadStaf();
            ViewBag.CustomerId = new SelectList(listcustomer, "CustomerId", "CustomerName");

            return View();
        }

        public ActionResult Edit(int id)
        {
            _repositoryCustomer = new RepositoryCustomer();
            _repositoryProject = new RepositoryProject();
            _repositoryStaff = new RepositoryStaff();

            var project = _repositoryProject.GetProjectByProjectId(id, p => p.ProjectStaffs);



            var listcustomer = _repositoryCustomer.GetListcustomer();
            ViewBag.CustomerId = new SelectList(listcustomer, "CustomerId", "CustomerName",project.CustomerId);

            loadStafEdit(project);
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit(Project project, string CMStaff, string PMStaff, string PDEStaff, string SEStaff, string QEStaff, string DRStaff)
        {
            _repositoryProject = new RepositoryProject();
            _repositoryCustomer = new RepositoryCustomer();
            int projectoldid = Convert.ToInt32(ViewBag.projectidold);

            var projectedit = _repositoryProject.GetProjectByProjectId(project.ProjectID, p => p.ProjectStaffs);

            projectedit.ProjectStaffs.CMStaff = CMStaff;
            projectedit.ProjectStaffs.PMStaff = PMStaff;
            projectedit.ProjectStaffs.PDEStaff = PDEStaff;
            projectedit.ProjectStaffs.SEStaff = SEStaff;
            projectedit.ProjectStaffs.QEStaff = QEStaff;
            projectedit.ProjectStaffs.DRStaff = DRStaff;
            projectedit.ProjectID = project.ProjectID;
            projectedit.Status = project.Status;
            projectedit.CustomerId = project.CustomerId;

            projectedit.ProjectStatus = Status.Modify;

            


            if (ModelState.IsValid)
            {
                projectedit.ProjectStatus = Status.Modify;
                OperationStatus ops = _repositoryProject.SaveProject(projectedit);
                if (ops.Status)
                {
                    return RedirectToAction("ProjectList", new { PMStaff = PMStaff});
                }
                else
                {
                    ModelState.AddModelError("", ops.ExceptionInnerMessage);
                }
            }
            var listcustomer = _repositoryCustomer.GetListcustomer();

            _repositoryStaff = new RepositoryStaff();
            loadStaf();
            ViewBag.CustomerId = new SelectList(listcustomer, "CustomerId", "CustomerName");

            return View();
        }

        public ActionResult ProjectListToPdf()
        {
           
            return new ActionAsPdf("_ProjectListPdf", null) { FileName = "ProjectPdf.pdf" };
        }

        #region metodos privados

        private void loadStaf(Project pojeft = null)
        {
            var liststaff = _repositoryStaff.GetListStaff();
            ViewBag.CMStaff = new SelectList(liststaff, "StaffName", "StaffName");

            ViewBag.PMStaff = new SelectList(liststaff, "StaffName", "StaffName");

            ViewBag.PDEStaff = new SelectList(liststaff, "StaffName", "StaffName");

            ViewBag.SEStaff = new SelectList(liststaff, "StaffName", "StaffName");

            ViewBag.QEStaff = new SelectList(liststaff, "StaffName", "StaffName");

            ViewBag.DRStaff = new SelectList(liststaff, "StaffName", "StaffName");
        }

        private void loadStafEdit(Project pojeft = null)
        {
            var liststaff = _repositoryStaff.GetListStaff();
            ViewBag.CMStaff = new SelectList(liststaff, "StaffName", "StaffName", pojeft.ProjectStaffs.CMStaff);

            ViewBag.PMStaff = new SelectList(liststaff, "StaffName", "StaffName", pojeft.ProjectStaffs.PMStaff);

            ViewBag.PDEStaff = new SelectList(liststaff, "StaffName", "StaffName", pojeft.ProjectStaffs.PDEStaff);

            ViewBag.SEStaff = new SelectList(liststaff, "StaffName", "StaffName", pojeft.ProjectStaffs.SEStaff);

            ViewBag.QEStaff = new SelectList(liststaff, "StaffName", "StaffName", pojeft.ProjectStaffs.QEStaff);

            ViewBag.DRStaff = new SelectList(liststaff, "StaffName", "StaffName", pojeft.ProjectStaffs.DRStaff);
        }
        private void loadPMStaff(string stafn)
        {
            _repositoryStaff = new RepositoryStaff();
            var liststaff = _repositoryStaff.GetListStaff();
            
            List<StatusPaged> liststatus = new List<StatusPaged>();
            liststatus.Add(new StatusPaged { Status = "A" });
            liststatus.Add(new StatusPaged { Status = "C" });
            liststatus.Add(new StatusPaged { Status = "H" });

            liststaff.Add(new Staff { StaffId = -1, StaffName = "ALL", StaffPosition = "ALL" });

            var staffname = stafn;
            if (staffname != null)
            {
                ViewBag.PMStaff = new SelectList(liststaff, "StaffName", "StaffName", staffname);
            }
            else
            {
                ViewBag.PMStaff = new SelectList(liststaff, "StaffName", "StaffName", "ALL");
            }
            
            ViewBag.Status = new SelectList(liststatus,"Status","Status","A");
        }

        private void loadCustomer() {
            var listcustomer = _repositoryCustomer.GetListcustomer();
            listcustomer.Add(new Customer { CustomerId = -1, CustomerName = "ALL" });
            ViewBag.CustomerId = new SelectList(listcustomer, "CustomerId", "CustomerName",-1);
        }

        #endregion
    }
}