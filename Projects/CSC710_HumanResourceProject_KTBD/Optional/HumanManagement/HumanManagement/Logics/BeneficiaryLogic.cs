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
    public class BeneficiaryLogic : AbstractLogic<BeneficiaryModel, BENEFICIARY, int>
    {
        protected override string GetJoinTable()
        {
            string joinTable = ", RELATION r where t.beneficiary_relationship = r.relation_id";
            return joinTable;
        }

        protected override string[] GetSelectPropNames(List<PropertyInfo> propList)
        {
            List<string> props = base.GetSelectPropNames(propList).ToList();
            props.Add("r.relation_name");
            return props.ToArray();
        }

        protected override BeneficiaryModel GetModelByReader(ref SqlDataReader reader)
        {
            BeneficiaryModel model = base.GetModelByReader(ref reader);
            model.RelationShipName = reader["relation_name"] as string;
            return model;
        }

        public List<BeneficiaryModel> GetListByEmployeeId(int employeeId)
        {
            return GetListAddClause(string.Format(" and t.beneficiary_emp_id = {0}", employeeId));
        }
    }
}