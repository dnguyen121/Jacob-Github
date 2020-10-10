using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanManagement.Models
{
    public class OverTimeModel
    {
        public int OvertimeId { get; set; }
        [Display(Name = "Overtime Date")]
        public System.DateTime OvertimeDate { get; set; }
        [Display(Name = "Number of Hours")]
        [Range(0, 8)]
        public decimal OvertimeHours { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(100, MinimumLength = 3)]
        public string OvertimeComment { get; set; }
        [Required]
        [Display(Name = "Employee")]
        public int OvertimeEmpId { get; set; }
        public string EmployeeName { get; set; }
    }
}