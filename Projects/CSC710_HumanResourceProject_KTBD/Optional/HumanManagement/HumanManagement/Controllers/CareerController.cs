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
    public class CareerController : Controller
    {
        private EmployeeLogic _logic = new EmployeeLogic();
        private SkillLogic _skillLogic = new SkillLogic();

        // GET: Carrer
        public ActionResult Index()
        {
            EmployeeModel employee = _logic.GetByEmail(User.Identity.Name);
            ViewBag.employee = employee;
            ViewBag.Skills = _skillLogic.GetSkill(employee.Employee_id);
            ViewBag.ExSkills = _skillLogic.GetExculdeSkill(employee.Employee_id);
            return View();
        }

        [HttpPost]
        public ActionResult Delete()
        {
            EmployeeModel employee = _logic.GetByEmail(User.Identity.Name);
            int id = int.Parse(Request.Form.GetValues("id")[0]);
            _skillLogic.DeleteSkill(id, employee.Employee_id);
            var Skills = _skillLogic.GetSkill(employee.Employee_id);
            var ExSkills = _skillLogic.GetExculdeSkill(employee.Employee_id);
            return Json(new { result = true, id = id, skills = Skills, exSkills = ExSkills });
        }

        [HttpPost]
        public ActionResult Add(int skillid)
        {
            EmployeeModel employee = _logic.GetByEmail(User.Identity.Name);
            _skillLogic.AddSkill(skillid, employee.Employee_id);
            SkillModel skill = _skillLogic.GetById(skillid);
            var Skills = _skillLogic.GetSkill(employee.Employee_id);
            var ExSkills = _skillLogic.GetExculdeSkill(employee.Employee_id);
            return Json(new { result = true, id = skillid, name = skill.SkillName, skills = Skills, exSkills = ExSkills });
        }
    }
}