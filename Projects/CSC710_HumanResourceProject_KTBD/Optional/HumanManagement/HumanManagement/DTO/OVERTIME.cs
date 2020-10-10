
namespace HumanManagement.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class OVERTIME
    {
        public int overtime_id { get; set; }
        public System.DateTime overtime_date { get; set; }
        public decimal overtime_hours { get; set; }
        public string overtime_comment { get; set; }
        public int overtime_emp_id { get; set; }
    }
}
