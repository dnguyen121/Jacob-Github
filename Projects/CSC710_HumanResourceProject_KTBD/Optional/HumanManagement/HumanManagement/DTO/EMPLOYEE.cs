namespace HumanManagement.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class EMPLOYEE
    {
    
        public int employee_id { get; set; }
        public string employee_SSN { get; set; }
        public string employee_name { get; set; }
        public System.DateTime employee_dob { get; set; }
        public string employee_sex { get; set; }
        public string employee_maritalStatus { get; set; }
        public string employee_image { get; set; }
        public string employee_phone { get; set; }
        public string employee_address { get; set; }
        public string employee_mail { get; set; }
        public System.DateTime employee_hireDate { get; set; }
        public string employee_superiorName { get; set; }
        public int employee_balances { get; set; }
        public int employee_dept_id { get; set; }
        public int employee_gralv_id { get; set; }
        public int employee_jb_id { get; set; }
        public int employee_workstt_id { get; set; }
   
    }
}
