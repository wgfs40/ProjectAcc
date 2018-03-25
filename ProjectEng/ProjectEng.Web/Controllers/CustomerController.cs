using ProjectEng.Models;
using ProjectEng.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEng.Web.Controllers
{
    
    public class CustomerController : Controller
    {
        private IRepositoryCustomer _repositorycustomer;

        // GET: Customer
        public ActionResult CustomerList()
        {
            _repositorycustomer = new RepositoryCustomer();
            var list = _repositorycustomer.GetListcustomer();
            return View(list);
        }

        public ActionResult CustomerCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerCreate(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _repositorycustomer = new RepositoryCustomer();
                OperationStatus ops = _repositorycustomer.SaveCustomer(customer);
                if (ops.Status)
                {
                    return RedirectToAction("CustomerList");
                }
                else
                {
                    ModelState.AddModelError("", ops.ExceptionInnerMessage);
                    return View();
                }

            }

            return View();
        }
    }
}