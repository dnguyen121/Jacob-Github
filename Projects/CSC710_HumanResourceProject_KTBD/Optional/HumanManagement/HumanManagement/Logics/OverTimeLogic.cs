using HumanManagement.DTO;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HumanManagement.Logics
{
    public class OverTimeLogic : AbstractLogic<OverTimeModel, OVERTIME, int>
    {
        protected override string[] GetSelectPropNames(List<PropertyInfo> propList)
        {
            List<string> propNames = base.GetSelectPropNames(propList).ToList();
            propNames.Add("e.employee_name");
            return propNames.ToArray();
        }

        protected override string GetJoinTable()
        {
            return ", EMPLOYEE e where t.overtime_emp_id = e.employee_id ";
        }

        protected override OverTimeModel GetModelByReader(ref SqlDataReader reader)
        {
            OverTimeModel model = base.GetModelByReader(ref reader);
            model.EmployeeName = reader["employee_name"] as string;
            return model;
        }

        public List<OverTimeModel> GetByEmployee(int userId, DateTime fromDate, DateTime toDate)
        {
            _connection.Open();
            string clause = $"{GetDefaultSelectClause()} and t.overtime_date >= @fromDate and t.overtime_date <= @toDate and e.employee_id = @id";
            SqlCommand command = new SqlCommand(clause, _connection);
            command.Parameters.AddWithValue("@id", userId);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            var reader = command.ExecuteReader();

            List<OverTimeModel> overTimes = new List<OverTimeModel>();
            while (reader.Read())
            {
                overTimes.Add(GetModelByReader(ref reader));
            }
            _connection.Close();
            return overTimes;
        }

        public List<OverTimeModel> GetOverTimeByMonth(int month, int year)
        {
            _connection.Open();
            DateTime fromDate = new DateTime(year, month, 1);
            DateTime toDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            string clause = $"{GetDefaultSelectClause()} and t.overtime_date >= @fromDate and t.overtime_date <= @toDate";
            SqlCommand command = new SqlCommand(clause, _connection);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            var reader = command.ExecuteReader();

            List<OverTimeModel> overTimes = new List<OverTimeModel>();
            while (reader.Read())
            {
                overTimes.Add(GetModelByReader(ref reader));
            }
            _connection.Close();
            return overTimes;
        }
    }
}