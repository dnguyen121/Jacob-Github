namespace HumanManagement.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class WEEKLYSALARY
    {
        public int weeklysalary_id { get; set; }
        public System.DateTime weeklysalary_startDate { get; set; }
        public System.DateTime weeklysalary_endDate { get; set; }
        public int weeklysalary_hours { get; set; }
        public int weeklysalary_emp_id { get; set; }
    }
}
