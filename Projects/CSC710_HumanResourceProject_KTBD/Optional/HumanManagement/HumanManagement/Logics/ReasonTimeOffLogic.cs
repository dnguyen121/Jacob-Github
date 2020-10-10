using HumanManagement.DTO;
using HumanManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanManagement.Logics
{
    public class ReasonTimeOffLogic : AbstractLogic<ResonTimeOffModel, REASON_TIMEOFF, int>
    {

        public List<ResonTimeOffModel> GetListForEmployee(int employeeId)
        {
            EmployeeLogic employeeLogic = new EmployeeLogic();
            EmployeeModel employee = employeeLogic.GetById(employeeId);
            List<ResonTimeOffModel> reasons = GetList();

            foreach(ResonTimeOffModel reason in reasons)
            {
                switch (reason.ReasonTimeoffId)
                {
                    case 1:
                        reason.AcceptPayType = "2";
                        break;
                    case 2:
                        reason.AcceptPayType = "2";
                        break;
                    case 3:
                        if (employee.EmployeeBalances > 0)
                        {
                            reason.AcceptPayType = "2";
                        }
                        else
                        {
                            reason.AcceptPayType = "1";
                        }
                        break;
                    case 4:
                        if (employee.EmployeeBalances > 0)
                        {
                            reason.AcceptPayType = "2";
                        }
                        else
                        {
                            reason.AcceptPayType = "1";
                        }
                        break;
                    default:
                        break;
                }
            }

            return reasons;
        }
    }
}