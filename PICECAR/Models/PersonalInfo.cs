using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    public class PersonalInfo
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PlaceOfBirth { get; set; }

        public DateTime? BirthDate { get; set; }

        public string HomeAddress { get; set; }

        public string BaguioAddress { get; set; }

        public string TelNum { get; set; }

        public string CellNum { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}