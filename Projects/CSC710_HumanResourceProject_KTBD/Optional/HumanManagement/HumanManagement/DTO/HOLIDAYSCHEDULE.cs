
namespace HumanManagement.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOLIDAYSCHEDULE
    {
    
        public int holidayschedule_id { get; set; }
        public System.DateTime holidayschedule_date { get; set; }
        public string holidayschedule_name { get; set; }
        public int holidayschedule_hours { get; set; }
    }
}
