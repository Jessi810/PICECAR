using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    // TODO: Changed some properties to enum
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        public string Course { get; set; }

        public string School { get; set; }

        public int? YearGraduated { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}