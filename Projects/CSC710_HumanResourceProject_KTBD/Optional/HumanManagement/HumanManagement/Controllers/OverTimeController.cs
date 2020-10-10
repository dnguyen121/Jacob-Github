using HumanManagement.Logics;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HumanManagement.Controllers
{
    [Authorize(Roles = "admin")]
    public class OverTimeController : Controller
    {
        OverTimeLogic _logic = new OverTimeLogic();
        EmployeeLogic _employeeLogic = new EmployeeLogic();

        public ActionResult Index()
        {
            AddViewBag(DateTime.Now.Month, DateTime.Now.Year);
            return View();
        }

        public ActionResult Manager(int month = 0, int year = 0)
        {
            if (month == 0 || year == 0)
            {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }
            ViewBag.List = _logic.GetOverTimeByMonth(month, year);
            ViewBag.Month = month;
            ViewBag.Year = year;
            return View();
        }

        [HttpPost ]
        public ActionResult Insert(OverTimeModel overTime)
        {
            if (ModelState.IsValid)
            {
                _logic.Create(overTime);
                return RedirectToAction("manager");
            }
            AddViewBag(overTime.OvertimeDate.Month,
                overTime.OvertimeDate.Year, overTime.OvertimeEmpId);
            return View("index");
        }

        private void AddViewBag(int month, int year, int employeeId = -1)
        {
            List<EmployeeModel> employeeList = _employeeLogic.GetList();
            ViewBag.EmployeeList = _employeeLogic.GetList(); 
            DateTime[] dateTimes = GetDateInMoth(month, year);
            ViewBag.DateInMonth = GetDateInMonthList(dateTimes[0], dateTimes[1]);
            if (employeeList.Count > 0)
            {
                if (employeeId == -1)
                {
                    employeeId = employeeList[0].Employee_id;
                }
                ViewBag.OverTimes = GetOverTimesList(employeeId, dateTimes[0], dateTimes[1]);
            }
            ViewBag.Month = month;
            ViewBag.Year = year;
        }

        public ActionResult GetOverTimeForUser(int employeeId, string monthYear)
        {
            int[] dates = monthYear.Split('/').Select(q => int.Parse(q)).ToArray();
            return Json(new { Table =  CreateMonthTable(employeeId, dates[1], dates[0])}
            , JsonRequestBehavior.AllowGet);
        }

        private string CreateMonthTable(int employeeId, int year, int month)
        {
            var fromDate = new DateTime(year, month, 1);
            var toDate = new DateTime(year, month
                , DateTime.DaysInMonth(year, month));
            bool start = true;
            bool end = false;
            List<OverTimeModel> overTimes = GetOverTimesList(employeeId, fromDate, toDate);
            var loopDate = fromDate;
            var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string s = "";
            while (!end)
            {
                s += "<tr>"; 
                for (int i = 0; i < 7; i++)
                {
                    if (loopDate > toDate)
                    {
                        end = true;
                        s += "<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                        continue;
                    }
                    if (start && i < (int)fromDate.DayOfWeek)
                    {
                        s += "<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                    }
                    if (!start || i >= (int)fromDate.DayOfWeek)
                    {
                        if (overTimes.Any(q => q.OvertimeDate.Day == loopDate.Day))
                        {
                            var overTime = overTimes.Where(q => q.OvertimeDate.Day == loopDate.Day).First();
                            s += $"<td class=\"bg-primary loopInfo\">{loopDate.Day}{CreateOverTimeTableInfo(overTime)}&nbsp;&nbsp;&nbsp;</td>";
                        }
                        else
                        {
                            if (loopDate > currentDate)
                            {
                                s += $"<td class=\"bg-dark\">{loopDate.Day} &nbsp;&nbsp;&nbsp;</td>";
                            }
                            else
                            {
                                s += $"<td class=\"bg-secondary change-date pointer\" value=\"{loopDate}\">{loopDate.Day} &nbsp;&nbsp;&nbsp;</td>";
                            }

                        }
                        loopDate = loopDate.AddDays(1);
                    }
                }
                s += "</tr>";
                start = false;
            }

            return s;
        }

        public string CreateOverTimeTableInfo(OverTimeModel overTime)
        {
            string s = "<div class=\"d-none position-absolute infoDiv bg-white text-black-50\">";
            s += "<table>";
            s += $"<tr><td>Number of Hourse</td><td>{overTime.OvertimeHours}</td></tr>";
            s += $"<tr><td>Employee Name</td><td>{overTime.EmployeeName}</td></tr>";
            s += $"<tr><td>Number of Hourse</td><td>{overTime.OvertimeComment}</td></tr>";
            s += "</table>";
            s += "</div>";
            return s;
        }

        public DateTime[] GetDateInMoth(int month, int year)
        {
            DateTime fromDate = new DateTime(year, month, 1);
            DateTime toDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return new DateTime[] { fromDate, toDate };
        }

        private List<OverTimeModel> GetOverTimesList(int employeeId, DateTime fromDate, DateTime toDate)
        {
            List<OverTimeModel> overTimes = _logic.GetByEmployee(employeeId, fromDate, toDate);
            return overTimes;
        }

        private List<string> GetDateInMonthList(DateTime fromDate, DateTime toDate)
        {
            List<string> dateInMonth = new List<string>();
            for (DateTime date = fromDate; fromDate >= toDate; fromDate.AddDays(1))
            {
                dateInMonth.Add(date.ToString("yyyy-MM-dd"));
            }
            return dateInMonth;
        }
    }
}