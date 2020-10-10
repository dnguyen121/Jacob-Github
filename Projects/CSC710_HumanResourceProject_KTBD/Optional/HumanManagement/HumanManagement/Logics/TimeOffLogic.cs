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
    public class TimeOffLogic : AbstractLogic<TimeOffModel, TIMEOFF, int>
    {

        protected override string[] GetSelectPropNames(List<PropertyInfo> propList)
        {
            List<string> propNames = base.GetSelectPropNames(propList).ToList();
            propNames.Add("e.employee_name");
            propNames.Add("r.reason_timeoff_name");
            propNames.Add("y.type_timeoff_name");
            return propNames.ToArray();
        }

        protected override string GetJoinTable()
        {
            return ", EMPLOYEE e, REASON_TIMEOFF r, TYPE_TIMEOFF y" +
                " where t.timeoff_emp_id = e.employee_id and" +
                " r.reason_timeoff_id = t.timeoff_res_id and y.type_timeoff_id = t.timeoff_tpe_id";
        }

        protected override TimeOffModel GetModelByReader(ref SqlDataReader reader)
        {
            TimeOffModel model = base.GetModelByReader(ref reader);
            model.ReasonName = reader["reason_timeoff_name"] as string;
            model.TypeName = reader["type_timeoff_name"] as string;
            return model;
        }

        public override void PostInsert(TimeOffModel model, TIMEOFF dto)
        {
            // auto decrease time off
            EmployeeLogic eLogic = new EmployeeLogic();
            var employee = eLogic.GetById(model.TimeoffEmpId);
            if (model.TimeoffResId == 3 || model.TimeoffResId == 4)
            {
                if (employee.EmployeeBalances > 0)
                {
                    dto.timeoff_tpe_id = 1;
                }
                else
                {
                    dto.timeoff_tpe_id = 2;
                }
                employee.EmployeeBalances -= (int)dto.timeoff_numberOfHours;
            } 
            else
            {
                dto.timeoff_tpe_id = 2;
            }
            eLogic.Edit(employee);
        }

        public List<TimeOffModel> GetListInWeek(int userId, DateTime fromDate)
        {
            _connection.Open();
            DateTime toDate = fromDate.AddDays(7);
            toDate = toDate > DateTime.Now ? DateTime.Now : toDate;

            string clause = $"{GetDefaultSelectClause()} and t.timeoff_date >= @fromDate and t.timeoff_date <= @toDate and e.employee_id = @id";
            SqlCommand command = new SqlCommand(clause, _connection);
            command.Parameters.AddWithValue("@id", userId);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            var reader = command.ExecuteReader();

            List<TimeOffModel> timeOffs = new List<TimeOffModel>();
            while (reader.Read())
            {
                timeOffs.Add(GetModelByReader(ref reader));
            }
            _connection.Close();
            return timeOffs;
        }

        /**
         * Get list time off by mounth and year
         * */
        public List<TimeOffModel> GetListInMouth(int month, int year, int employeeId)
        {
            _connection.Open();
            DateTime fromDate = new DateTime(year, month, 1);
            DateTime toDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            string clause = $"{GetDefaultSelectClause()} and t.timeoff_date >= @fromDate and t.timeoff_date <= @toDate and e.employee_id = @id";
            SqlCommand command = new SqlCommand(clause, _connection);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate", toDate);
            command.Parameters.AddWithValue("@id", employeeId);
            var reader = command.ExecuteReader();

            List<TimeOffModel> timeOffs = new List<TimeOffModel>();
            while (reader.Read())
            {
                timeOffs.Add(GetModelByReader(ref reader));
            }
            _connection.Close();
            return timeOffs;
        }
    }
}