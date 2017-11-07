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

        public string EmploymentSector { get; set; }

        public string OtherEmploymentSector { get; set; }

        public string AreaOfPractice { get; set; }

        public string OtherAreaOfPractice { get; set; }

        public string CurrentCompany { get; set; }

        public string EmployeeType { get; set; }

        public string CurrentJobPosition { get; set; }

        public string WorkAddress { get; set; }

        public string CompanyTelNum { get; set; }

        public string CompanyFaxNum { get; set; }

        public string CompanyEmail { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}