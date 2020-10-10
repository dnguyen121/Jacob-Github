
namespace HumanManagement.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class TIMEOFF
    {
        public int timeoff_id { get; set; }
        public System.DateTime timeoff_date { get; set; }
        public decimal timeoff_numberOfHours { get; set; }
        public int timeoff_res_id { get; set; }
        public int timeoff_tpe_id { get; set; }
        public string timeoff_comment { get; set; }
        public int timeoff_emp_id { get; set; }
    }
}
