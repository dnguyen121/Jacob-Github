using HumanManagement.DTO;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HumanManagement.Logics
{
    public class GradeLevelLogic : AbstractLogic<GradeLevelModel, GRADELEVEL, int>
    {

        public GradeLevelModel GetForEmployee(int employeeId)
        {
            _connection.Open();
            string selectClause = "SELECT gl.gradelevel_id, gl.gradelevel_payRate FROM EMPLOYEE e, GRADELEVEL gl WHERE e.employee_gralv_id = gl.gradelevel_id AND e.employee_id = @id";
            SqlCommand command = new SqlCommand(selectClause, _connection);
            command.Parameters.AddWithValue("@id", employeeId);
            var reader = command.ExecuteReader();
            GradeLevelModel grade = null;
            while (reader.Read())
            {
                grade = GetModelByReader(ref reader);
            }
            return grade;
        }
    }
}