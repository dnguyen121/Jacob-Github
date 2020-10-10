using HumanManagement.Logics;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HumanManagement.Controllers
{
    public class WeeklySalaryController : Controller
    {
        private readonly TimeOffLogic _timeOffLogic = new TimeOffLogic();
        private readonly OverTimeLogic _overTimeLogic = new OverTimeLogic();
        private readonly EmployeeLogic _employeeLogic = new EmployeeLogic();
        private readonly GradeLevelLogic _gradeLevelLogic = new GradeLevelLogic();

        public static int WORK_TIME = 8;

        // GET: WeeklySalary
        public ActionResult Index()
        {
            DateTime fromDate = DateTime.Now;
            fromDate = fromDate.AddDays(-(int)fromDate.DayOfWeek);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);

            CalculateWeeklySalary(fromDate);
            return View();
        }

        public ActionResult SelectDate(String fromDateString)
        {
            DateTime fromDate = DateTime.Now;
            DateTime.TryParse(fromDateString, out fromDate);
            CalculateWeeklySalary(fromDate);
            return View("Index");
        }

        public void CalculateWeeklySalary(DateTime fromDate)
        {
            decimal defaultWorkTime = 5 * WORK_TIME;

            // Calculate sum time off
            EmployeeModel employee = _employeeLogic.GetByEmail(User.Identity.Name);
            List<TimeOffModel> timeOffModels = _timeOffLogic.GetListInWeek(employee.Employee_id, fromDate);
            decimal timeOffNoPay = timeOffModels
                .Where(q => q.TimeoffTpeId == 2)
                .Select(q => q.TimeoffNumberOfHours).DefaultIfEmpty()
                .Sum();
            ViewBag.TimeOffNoPay = timeOffNoPay;
            ViewBag.TimeOffNoPayList = timeOffModels.Where(q => q.TimeoffTpeId == 2).ToList();
            decimal timeOffPay = timeOffModels
                .Where(q => q.TimeoffTpeId == 1)
                .Select(q => q.TimeoffNumberOfHours).DefaultIfEmpty()
                .Sum();
            var toDate = fromDate.AddDays(7);
            toDate = toDate <= DateTime.Now ? toDate : DateTime.Now; 
            List<OverTimeModel> overTimeModels = _overTimeLogic.GetByEmployee(employee.Employee_id, fromDate, toDate);
            ViewBag.TimeOffPay = timeOffPay;
            ViewBag.TimeOffPayList = timeOffModels.Where(q => q.TimeoffTpeId == 1).ToList();

            // Calculate sum of over time
            decimal overTime = overTimeModels.Select(q => q.OvertimeHours).DefaultIfEmpty(0).Sum();
            ViewBag.OverTime = overTime;
            decimal totalTime = defaultWorkTime + overTime;
            ViewBag.WorkTime = defaultWorkTime - timeOffPay - timeOffNoPay;

            // Calculate each time percent
            ViewBag.timeOffNoPayPercent = (int)((timeOffNoPay / totalTime) * 100);
            ViewBag.timeOffPayPercent = (int)((timeOffPay / totalTime) * 100);
            ViewBag.normalPayPercent = (int)((defaultWorkTime / totalTime) * 100);
            ViewBag.overTimePayPercent = (int)((overTime / totalTime) * 100);

            ViewBag.TimeOffList = timeOffModels;
            ViewBag.OverTimeList = overTimeModels;

            // Caculate salary
            GradeLevelModel gradeLevel = _gradeLevelLogic.GetForEmployee(employee.Employee_id);
            decimal salary = (defaultWorkTime - timeOffNoPay) * gradeLevel.GradelevelPayRate;
            salary += overTime * (decimal)1.5 * gradeLevel.GradelevelPayRate;
            ViewBag.WeeklySalary = salary;

            List<DateTime> startWeekDates = new List<DateTime>();
            DateTime startWeekDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            while(startWeekDate.DayOfWeek != DayOfWeek.Sunday)
            {
                startWeekDate = startWeekDate.AddDays(1);
            }
            while (startWeekDate <= DateTime.Now)
            {
                startWeekDates.Add(startWeekDate);
                startWeekDate = startWeekDate.AddDays(7);
            }
            ViewBag.StartWeekDates = startWeekDates.Select(q => q.ToString("yyyy-MM-dd"));
            ViewBag.selectedDate = fromDate.ToString("yyyy-MM-dd");
        }
    }
}