using PICECAR.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    // TODO: Changed some properties to enum
    public class Profession
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        [Display(Name = "Employment Sector")]
        public EnumData.EmploymentSector? EmploymentSector { get; set; }

        [Display(Name = "Other")]
        public string OtherEmploymentSector { get; set; }

        [Display(Name = "Area of Practice")]
        public EnumData.AreaOfPractice? AreaOfPractice { get; set; }

        [Display(Name = "Other")]
        public string OtherAreaOfPractice { get; set; }

        [Display(Name = "Current Company")]
        public string CurrentCompany { get; set; }

        [Display(Name = "Employee Type")]
        public EnumData.EmployeeType? EmployeeType { get; set; }

        [Display(Name = "Current Job Position")]
        public string CurrentJobPosition { get; set; }

        [Display(Name = "Company/Work Address")]
        public string WorkAddress { get; set; }

        [Display(Name = "Company Telephone #")]
        public string CompanyTelNum { get; set; }

        [Display(Name = "Company Tele-fax")]
        public string CompanyFaxNum { get; set; }

        [Display(Name = "Company Email")]
        public string CompanyEmail { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}