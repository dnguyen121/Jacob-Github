using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanManagement.Models
{
    public class BeneficiaryModel
    {
        public int BeneficiaryId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string BeneficiaryFName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string BeneficiaryLName { get; set; }
        [Required]
        [Display(Name = "Relation")]
        public int BeneficiaryRelationship { get; set; }
        public string RelationShipName { get; set; }
        [Required]
        [RegularExpression("^[0-9]{10}$")]
        [Display(Name = "Phone Number")]
        public string BeneficiaryContact { get; set; }
        [Required]
        [Display(Name = "Address Line 1")]
        public string BeneficiaryAddressLine { get; set; }
        [Required]
        [Display(Name = "City")]
        public string BeneficiaryCity { get; set; }
        [Required]
        [Display(Name = "State")]
        public string BeneficiaryState { get; set; }
        [Required]
        [RegularExpression("^(?=.*[1-9].*)[0-9]{5}$")]
        [Display(Name = "Zip code")]
        public string BeneficiaryZipCode { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string BeneficiaryCountry { get; set; } = "United States of America";
        [EmailAddress]
        [Display(Name = "Email")]
        public string BeneficiaryEmail { get; set; }
        public int BeneficiaryEmpId { get; set; }

        public string BeneficiaryAddress { 
            get
            {
                return string.Join(", ", BeneficiaryAddressLine, BeneficiaryCity, BeneficiaryState, BeneficiaryZipCode, BeneficiaryCountry);
            }
        }
    }
}