using HumanManagement.DTO;
using HumanManagement.Models;
using HumanManagement.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace HumanManagement.Logics
{
    public class EmployeeLogic : AbstractLogic<EmployeeModel, EMPLOYEE, int>
    {

        protected override string GetDefaultSelectClause()
        {
            string selectClause = $"select {string.Join(", ", GetSelectPropNames(GetSelectProp()))}, d.department_name, g.gradelevel_payRate, j.job_name, w.workingstatus_name";
            selectClause += " from Employee t, DEPARTMENT d, JOB j, GRADELEVEL g, WORKINGSTATUS w ";
            selectClause += " where t.employee_dept_id = d.department_id and t.employee_jb_id = j.job_id and t.employee_gralv_id = g.gradelevel_id and t.employee_workstt_id = w.workingstatus_id";
            return selectClause;
        }

        protected override EmployeeModel GetModelByReader(ref SqlDataReader reader)
        {
            EmployeeModel m = base.GetModelByReader(ref reader);
            m.DeptName = reader["department_name"] as string;
            m.WorkingStatus = reader["workingstatus_name"] as string;
            m.Job = reader["job_name"] as string;
            m.GradLevel = (int)reader["gradelevel_payRate"];
            return m;
        }

        public EmployeeModel GetByEmail(string email)
        {
            _connection.Open();
            string selectClause = GetDefaultSelectClause();
            selectClause = $"{selectClause} and t.employee_mail = @email";
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = selectClause;
            command.Parameters.AddWithValue("@email", email);
            var reader = command.ExecuteReader();

            EmployeeModel employee = null;

            while (reader.Read())
            {
                employee = GetModelByReader(ref reader);
            }
            _connection.Close();
            return employee;
        }

        public List<EmployeeModel> GetOrganizationTree(int employeeId)
        {
            List<EmployeeModel> tree = new List<EmployeeModel>();
            EmployeeModel model = GetById(employeeId);
            List<EmployeeModel> top = GetListAddClause(string.Format(" and t.employee_name = '{0}'", model.Employee_superiorName));
            tree.AddRange(top);
            if (top.Count > 0)
            {
                List<EmployeeModel> middle = GetListAddClause(string.Format(" and t.employee_superiorName = '{0}'", top.First().Employee_name));
                tree.AddRange(middle);
            }
            else
            {
                tree.Add(model);
            }
            List<EmployeeModel> lasted = GetListAddClause(string.Format(" and t.employee_superiorName = '{0}'", model.Employee_name));
            tree.AddRange(lasted);

            return tree;
        }

        public override void PreInsert(EmployeeModel model)
        {
            model.EmployeeBalances = 128;
        }

        public override void PostInsert(EmployeeModel model, EMPLOYEE dto)
        {
            var manager = new ApplicationUserManager(
                new UserStore<ApplicationUser>(
                    HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>()));

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Employee_mail,
                Email = model.Employee_mail
            };
            manager.Create(user, ApplicationUserManager.DefaultPassword);
        }
    }
}