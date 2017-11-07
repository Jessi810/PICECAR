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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Birthdate")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }

        [Display(Name = "Baguio Address")]
        public string BaguioAddress { get; set; }

        [Display(Name = "Telephone Number")]
        public string TelNum { get; set; }

        [Display(Name = "Cellphone Number")]
        public string CellNum { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}