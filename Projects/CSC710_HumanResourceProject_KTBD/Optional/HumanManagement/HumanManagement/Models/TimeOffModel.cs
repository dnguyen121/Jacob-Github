using System;
using System.ComponentModel.DataAnnotations;

namespace HumanManagement.Models
{
    public class TimeOffModel
    {
        public int TimeoffId { get; set; }
        public System.DateTime TimeoffDate { get; set; } = DateTime.Now;
        [Display(Name = "Number of Hours")]
        [Range(0, 8)]
        public decimal TimeoffNumberOfHours { get; set; }
        [Display(Name = "Reason")]
        public int TimeoffResId { get; set; }
        [Display(Name = "Type")]
        public int TimeoffTpeId { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(100, MinimumLength = 3)]
        public string TimeoffComment { get; set; }
        public int TimeoffEmpId { get; set; }
        public string ReasonName { get; set; }
        public string TypeName { get; set; }
    }
}