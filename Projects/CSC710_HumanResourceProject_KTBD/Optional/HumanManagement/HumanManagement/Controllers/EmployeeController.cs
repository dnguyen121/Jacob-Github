using HumanManagement.DTO;
using HumanManagement.Logics;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HumanManagement.Utils.CustomHelper;

namespace HumanManagement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly EmployeeLogic _logic = new EmployeeLogic();
        private readonly JobLogic _jobLogic = new JobLogic();
        private readonly GradeLevelLogic _gradeLevelLogic = new GradeLevelLogic();
        private readonly DeparmentLogic _deparmentLogic = new DeparmentLogic();
        private readonly WorkingStatusLogic _workingStatusLogic = new WorkingStatusLogic();
        private static string ImageFolder = "Content/images";

        // GET: Employee
        public ActionResult Index()
        {
            EmployeeModel employee = _logic.GetByEmail(User.Identity.Name);
            ViewBag.employee = employee;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Manager()
        {
            ViewBag.List = _logic.GetList();
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Insert()
        {
            AddInsertViewBag();
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Insert(EmployeeModel model)
        {
            if (Request.Files.Count > 0)
            {
                if (Request.Files[0].ContentLength == 0 && string.IsNullOrEmpty(model.Employee_image))
                {
                    ModelState.AddModelError(nameof(EmployeeModel.Employee_image), "Image is required");
                }
            }
            if (ModelState.IsValid)
            {
                model.Employee_image = SaveImage();
                _logic.Create(model);
                return RedirectToAction("manager");
            }
            AddInsertViewBag();
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public ActionResult CheckExistedEmail(string Employee_mail)
        {
            if (_logic.GetByEmail(Employee_mail) != null)
            {
                return Json($"the email {Employee_mail} already exists.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private void AddInsertViewBag()
        {
            ViewBag.SexOptions = EmployeeModel.SexOptions;
            ViewBag.MarialStatusOptions = EmployeeModel.MarialStatusOptions;
            List<SelectOption> SuperiorNameOptions = _logic.GetList().Select(q => new SelectOption(q.Employee_name)).ToList();
            SuperiorNameOptions.Insert(0, new SelectOption(null, ""));

            ViewBag.SuperiorNameOptions = SuperiorNameOptions;

            ViewBag.DeparmentOptions = _deparmentLogic.GetList().Select(q => 
            new SelectOption()
            {
                Display = q.DepartmentName.ToString(),
                Value = q.DepartmentId.ToString(),
            }).ToList();

            ViewBag.GradeLevelOptions = _gradeLevelLogic.GetList().Select(q =>
            new SelectOption()
            {
                Display = q.GradelevelPayRate.ToString(),
                Value = q.GradelevelId.ToString(),
            }).ToList();

            ViewBag.JobOptions = _jobLogic.GetList().Select(q => new SelectOption()
            {
                Display = q.JobName.ToString(),
                Value = q.JobId.ToString(),
            }).ToList();

            ViewBag.WorkingStatusOptions = _workingStatusLogic.GetList().Select(q => new SelectOption()
            {
                Display = q.WorkingstatusName.ToString(),
                Value = q.WorkingstatusId.ToString(),
            }).ToList();
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    string id = Request.Form.Get("Employee_id");

                    EmployeeModel employee = _logic.GetById(int.Parse(id));
                    employee.Employee_image = SaveImage();
                    _logic.Edit(employee);

                    return Json(new { result = 1, message = $"{employee.Employee_image}" });
                }
                else
                {
                    return Json(new { result = 0, message = "File not found!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = 0, message = ex.Message });
            }
        }

        private string SaveImage()
        {
            var file = Request.Files[0];
            string id = Request.Form.Get("Employee_id");

            var fileName = $"image{DateTime.Now.ToString("MMddyyyyhhmmssfff")}{Path.GetExtension(file.FileName)}";

            var path = Path.Combine(Server.MapPath($"~/{ImageFolder}"), fileName);
            file.SaveAs(path);
            string imagePath = $"/{ImageFolder}/{fileName}";
            return imagePath;
        }

    }
}