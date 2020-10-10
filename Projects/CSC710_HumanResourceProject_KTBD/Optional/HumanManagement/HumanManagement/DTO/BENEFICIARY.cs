namespace HumanManagement.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class BENEFICIARY
    {
        public int beneficiary_id { get; set; }
        public string beneficiary_fName { get; set; }
        public string beneficiary_lName { get; set; }
        public int beneficiary_relationship { get; set; }
        public string beneficiary_contact { get; set; }
        public string beneficiary_addressLine { get; set; }
        public string beneficiary_city { get; set; }
        public string beneficiary_state { get; set; }
        public string beneficiary_zipCode { get; set; }
        public string beneficiary_country { get; set; }
        public string beneficiary_email { get; set; }
        public int beneficiary_emp_id { get; set; }
   
    }
}
