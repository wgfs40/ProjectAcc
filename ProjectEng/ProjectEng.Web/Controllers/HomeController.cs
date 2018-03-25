using ProjectEng.Models;
using ProjectEng.Repositories.Repository;
using ProjectEng.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Rotativa.Options;

namespace ProjectEng.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private IRepositoryProject _repositoryProject;
        private IRepositoryTask _repositoryTask;
        private IRepositoryCustomer _repositoryCustomer;
        private IRepositoryStaff _repositoryStaff;

        private const int pagedcount = 10;

        public ActionResult Index(string sortparam, string PMStaff, string Status, string customeridpage, string pMStaffpage, string statuspage, int? page, int customerid = -1)
        {
            _repositoryProject = new RepositoryProject();
            _repositoryCustomer = new RepositoryCustomer();

            loadCustomer();
            loadPMStaff();

            ViewBag.Customeridpage = customeridpage ?? customerid.ToString();
            ViewBag.PMStaffpage = pMStaffpage ?? PMStaff;
            ViewBag.sortCode = string.IsNullOrEmpty(sortparam) ? "ProjectID" : "";
            ViewBag.CustomerName = string.IsNullOrEmpty(sortparam) ? "CustomerName" : "";

            var statuspaged = Status ?? "A";
            ViewBag.Statuspage = statuspage ?? statuspaged;
            ViewBag.CurrentFilter = sortparam;
            
            var pagednumber = page ?? 1;
            if (string.IsNullOrEmpty(PMStaff) && string.IsNullOrEmpty(pMStaffpage))
            {
                var list = _repositoryProject.GetListProject(sortparam,statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);
                return View(list);
            }
            else
            {
                if (customerid != -1)
                {
                    if (!PMStaff.Equals("ALL"))
                    {
                        customerid = customerid != -1? customerid : int.Parse(customeridpage);
                        PMStaff = pMStaffpage;
                        Status = statuspage;

                        var list = _repositoryProject.GetListProjectByCustomerId(sortparam, customerid, PMStaff, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);

                        return View(list);
                    }
                    else
                    {
                        customerid = customerid != -1 ? customerid : int.Parse(customeridpage);
                        Status = statuspage;
                        var list = _repositoryProject.GetListProjectByCustomerId(sortparam, customerid, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);
                        return View(list);
                    }

                }

                PMStaff = pMStaffpage ?? PMStaff;
                if (PMStaff.Equals("ALL"))
                {
                    Status = statuspage;
                    var list = _repositoryProject.GetListProject(sortparam, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);
                    return View(list);
                }
                else
                {
                    
                    Status = statuspage;
                    var list = _repositoryProject.GetListProjectByPM(sortparam, PMStaff, statuspaged, pagednumber, pagedcount, p => p.Customers, p => p.ProjectStaffs);
                    return View(list);
                }
                
               
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult viewTask(int id)
        {
            _repositoryTask = new RepositoryTask();
            var list = _repositoryTask.GetListTaskByProjectId(id);
            return PartialView(list);
        }

        public ActionResult DashboardToPdf(string page)
        {
            _repositoryProject = new RepositoryProject();
            var sortparam = "ProjectID";
            var statuspaged = "A";
            var pagednumber = 1;
            var pagetotal = int.Parse(page);

            var model = _repositoryProject.GetListProject(sortparam, statuspaged, pagednumber, pagetotal, p => p.Customers, p => p.ProjectStaffs);
            return new PartialViewAsPdf("_DashboardPdf", model) {
                FileName = "_DashboardPdf.pdf",
                PageSize = Size.Letter,
                PageOrientation = Orientation.Portrait,
                MinimumFontSize = 14,
                PageMargins = { Top = 20, Bottom = 10 },
                PageHeight = 40,
                CustomSwitches =
                    "--footer-center \"Name: " + "_DashboardPdf" + "  DOS: " +
                    DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };
        }
        #region private metoh
        private void loadPMStaff()
        {   
            _repositoryStaff = new RepositoryStaff();
            var liststaff = _repositoryStaff.GetListStaff();

            List<StatusPaged> liststatus = new List<StatusPaged>();
            liststatus.Add(new StatusPaged { Status = "A" });
            liststatus.Add(new StatusPaged { Status = "C" });
            liststatus.Add(new StatusPaged { Status = "H" });

            liststaff.Add(new Staff { StaffId = -1, StaffName = "ALL", StaffPosition = "ALL" });

            ViewBag.PMStaff = new SelectList(liststaff, "StaffName", "StaffName", "ALL");
            ViewBag.Status = new SelectList(liststatus, "Status", "Status", "A");
        }

        private void loadCustomer()
        {
            _repositoryCustomer = new RepositoryCustomer();
            var listcustomer = _repositoryCustomer.GetListcustomer();
            listcustomer.Add(new Customer { CustomerId = -1, CustomerName = "ALL" });
            ViewBag.CustomerId = new SelectList(listcustomer, "CustomerId", "CustomerName", -1);
        }
        #endregion
    }
}