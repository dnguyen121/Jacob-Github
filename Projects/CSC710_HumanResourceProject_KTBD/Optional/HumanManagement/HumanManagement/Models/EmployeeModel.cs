using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HumanManagement.Utils.CustomHelper;

namespace HumanManagement.Models
{
    public class EmployeeModel
    {
        public static readonly List<SelectOption> SexOptions = new List<SelectOption>()
        {
            new SelectOption("Male"), 
            new SelectOption("Female"),
            new SelectOption("Other")
        };

        public static readonly List<SelectOption> MarialStatusOptions = new List<SelectOption>()
        {
            new SelectOption("Single"),
            new SelectOption("Married")
        };

        public int Employee_id { get; set; }

        [RegularExpression("^[0-9]{9}$")]
        [Required]
        [Display(Name = "Employee SSN")]
        public string Employee_SSN { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        [Display(Name = "Employee Name")]
        public string Employee_name { get; set; }

        [Required]
        [Display(Name = "Day of Birth")]
        public System.DateTime Employee_dob { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Employee_sex { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public string Employee_maritalStatus { get; set; }
        public string Employee_image { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$")]
        [Display(Name = "Phone Number")]
        public string Employee_phone { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Adress")]
        public string Employee_address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote(action: "CheckExistedEmail", controller: "Employee")]
        public string Employee_mail { get; set; }

        [Required]
        [Display(Name = "Hire Date")]
        public DateTime Employee_hireDate { get; set; }

        [Required]
        [Display(Name = "Deparment")]
        public int Employee_dept_id { get; set; }

        [Required]
        [Display(Name = "Grad level")]
        public int Employee_gralv_id { get; set; }

        [Required]
        [Display(Name = "Job")]
        public int Employee_jb_id { get; set; }

        [Required]
        [Display(Name = "Working Status")]
        public int Employee_workstt_id { get; set; }

        [Display(Name = "Superior Name")]
        public string Employee_superiorName { get; set; }
        public string DeptName { get; set; }
        public int GradLevel { get; set; }
        public string Job { get; set; }
        public string WorkingStatus { get; set; }
        public int EmployeeBalances { get; set; }
    }
}