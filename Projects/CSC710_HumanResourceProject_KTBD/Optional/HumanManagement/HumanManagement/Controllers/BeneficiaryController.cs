using HumanManagement.Logics;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanManagement.Controllers
{
    [Authorize]
    public class BeneficiaryController : Controller
    {
        private readonly BeneficiaryLogic _logic;
        private readonly EmployeeLogic _employeeLogic;
        private readonly RelationLogic _relationLogic;
        private readonly StateLogic _stateLogic;

        public BeneficiaryController()
        {
            _logic = new BeneficiaryLogic();
            _employeeLogic = new EmployeeLogic();
            _relationLogic = new RelationLogic();
            _stateLogic = new StateLogic();
        }

        public ActionResult Index()
        {
            var employee = _employeeLogic.GetByEmail(User.Identity.Name);
            ViewBag.List = _logic.GetListByEmployeeId(employee.Employee_id);
            return View();
        }

        [HttpGet]
        public ActionResult Insert()
        {
            BindingList();
            return View();
        }

        [HttpPost]
        public ActionResult Insert(BeneficiaryModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeLogic.GetByEmail(User.Identity.Name);
                model.BeneficiaryEmpId = employee.Employee_id;
                _logic.Create(model);
                return RedirectToAction("index");
            }
            BindingList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.EditMode = 1;
            BeneficiaryModel model = _logic.GetById(id);
            if (_logic.GetById(id) == null)
            {
                return RedirectToAction("index");
            }
            BindingList();
            return View("insert", model);
        }

        [HttpPost]
        public ActionResult Edit(BeneficiaryModel model)
        {
            ViewBag.EditMode = 1;
            if (ModelState.IsValid)
            {
                var employee = _employeeLogic.GetByEmail(User.Identity.Name);
                model.BeneficiaryEmpId = employee.Employee_id;
                _logic.Edit(model);
                return RedirectToAction("index");
            }
            BindingList();
            return View("insert", model);
        }

        public void BindingList()
        {
            ViewBag.StateList = _stateLogic.GetList();
            ViewBag.RelationList = _relationLogic.GetList();
        }

        public ActionResult Delete(int id)
        {
            _logic.Delete(id);
            return RedirectToAction("index");
        }
    }
}