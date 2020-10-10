using HumanManagement.DTO;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HumanManagement.Logics
{
    public class SkillLogic : AbstractLogic<SkillModel, SKILL, int>
    {
        public List<SkillModel> GetSkill(int employeeId)
        {
            _connection.Open();
            string selectClause = "SELECT s.skill_id, s.skill_name FROM EMPLOYEE e, EMP_HAS_SKILL es, SKILL s WHERE e.employee_id = es.emp_id AND s.skill_id = es.skl_id AND es.emp_id = @id";
            var skills = new List<SkillModel>();
            SqlCommand command = new SqlCommand(selectClause, _connection);
            command.Parameters.AddWithValue("@id", employeeId);
            var reader = command.ExecuteReader();

            while(reader.Read())
            {
                SkillModel model = GetModelByReader(ref reader);
                skills.Add(model);
            }

            _connection.Close();
            return skills;
        }

        public List<SkillModel> GetExculdeSkill(int employeeId)
        {
            _connection.Open();
            string selectClause = "SELECT s1.skill_id, s1.skill_name FROM SKILL s1 WHERE s1.skill_id IN (SELECT s2.skill_id FROM SKILL s2 EXCEPT SELECT skl_id FROM EMP_HAS_SKILL WHERE emp_id = @id)";
            var skills = new List<SkillModel>();
            SqlCommand command = new SqlCommand(selectClause, _connection);
            command.Parameters.AddWithValue("@id", employeeId);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                SkillModel model = GetModelByReader(ref reader);
                skills.Add(model);
            }

            _connection.Close();
            return skills;
        }

        public void DeleteSkill(int skillId, int employeeId)
        {
            _connection.Open();
            string deleteClause = "DELETE FROM EMP_HAS_SKILL WHERE emp_id = @empId AND skl_id = @sklId";
            SqlCommand command = new SqlCommand(deleteClause, _connection);
            command.Parameters.AddWithValue("@empId", employeeId);
            command.Parameters.AddWithValue("@sklId", skillId);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void AddSkill(int skillId, int employeeId)
        {
            _connection.Open();
            string addClause = "INSERT EMP_HAS_SKILL(emp_id, skl_id) VALUES (@empId, @sklId)";
            SqlCommand command = new SqlCommand(addClause, _connection);
            command.Parameters.AddWithValue("@empId", employeeId);
            command.Parameters.AddWithValue("@sklId", skillId);
            command.ExecuteNonQuery();
            _connection.Close();
        }

    }
}