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
    public class OrigizationController : Controller
    {
        EmployeeLogic _logic = new EmployeeLogic();


        public ActionResult Index()
        {
            EmployeeModel model = _logic.GetByEmail(User.Identity.Name);
            var organizationTree = _logic.GetOrganizationTree(model.Employee_id);
            string employeeJson = "";
            List<string> employeeNames = new List<string>();
            organizationTree[0].Employee_superiorName = null;
            foreach (EmployeeModel employee in organizationTree)
            {
                var name = employee.Employee_name.Replace(" ", "_");
                employeeNames.Add(name);
                string employeeString = $"{name} = {{";
                if (employee.Employee_superiorName != null)
                {
                    employeeString += $"parent: {employee.Employee_superiorName.Replace(" ", "_")},";
                }
                employeeString += $"text: {{";
                employeeString += $"name : \"{employee.Employee_name}\",";
                employeeString += $"title : \"{employee.Job}\",";
                employeeString += $"contact : \"tel : {employee.Employee_phone}\"";
                employeeString += $"}},";
                employeeString += $"image : \"{employee.Employee_image}\"}},";
                employeeJson += employeeString;
            }
            ViewBag.employeeJson = employeeJson;
            ViewBag.employeeNames = string.Join(",", employeeNames);
            return View();
        }
    }
}