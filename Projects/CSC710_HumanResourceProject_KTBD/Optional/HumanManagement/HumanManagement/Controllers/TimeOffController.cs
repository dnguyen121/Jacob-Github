using HumanManagement.Logics;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanManagement.Controllers
{
    public class TimeOffController : Controller
    {
        private readonly TimeOffLogic _logic;
        private readonly ReasonTimeOffLogic _reasonTimeOffLogic;
        private readonly TypeTimeOffLogic _typeTimeOffLogic;
        private readonly EmployeeLogic _employeeLogic;

        public TimeOffController()
        {
            _logic = new TimeOffLogic();
            _reasonTimeOffLogic = new ReasonTimeOffLogic();
            _typeTimeOffLogic = new TypeTimeOffLogic();
            _employeeLogic = new EmployeeLogic();
        }

        public ActionResult Index()
        {
            BindingList(DateTime.Now.Month, DateTime.Now.Year);
            return View();
        }

        public ActionResult ChangeMonth(TimeOffModel model)
        {
            try
            {
                string[] mounths = Request.Form.GetValues("monthYear")[0].Split('/');
                int month = int.Parse(mounths[0]);
                int year = int.Parse(mounths[1]);
                BindingList(month, year);
            } 
            catch
            {
                return RedirectToAction("index");
            }
            return View("index", model);
        }

        [HttpPost]
        public ActionResult Insert(TimeOffModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeModel employee = _employeeLogic.GetByEmail(User.Identity.Name);
                model.TimeoffEmpId = employee.Employee_id;
                _logic.Create(model);
                return RedirectToAction("index");
            }
            BindingList(model.TimeoffDate.Month, model.TimeoffDate.Year);
            return View("index");
        }

        public void BindingList(int month, int year)
        {
            var user = _employeeLogic.GetByEmail(User.Identity.Name);
            ViewBag.Balances = user.EmployeeBalances;
            ViewBag.ReasonList = _reasonTimeOffLogic.GetListForEmployee(user.Employee_id);
            ViewBag.TypeList = _typeTimeOffLogic.GetList();
            ViewBag.ListInMonth = _logic.GetListInMouth(month, year, user.Employee_id);
            ViewBag.Month = month;
            ViewBag.Year = year;
        }
    }
}