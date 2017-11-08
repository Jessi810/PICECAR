using PICECAR.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    public class Profile
    {
        public PersonalInfo PersonalInfo { get; set; }
        public MembershipInfo MembershipInfo { get; set; }
        public List<Education> Education { get; set; }
        public Profession Profession { get; set; }

        //// 
        //[Display(Name = "Course/Degree")]
        //public string Course { get; set; }

        //[Display(Name = "School")]
        //public string School { get; set; }

        //[Display(Name = "Year Graduated")]
        //public int? YearGraduated { get; set; }


        //// 
        //[Display(Name = "PRC #")]
        //public string PrcNum { get; set; }

        //[Display(Name = "PRC Date Issued")]
        //public DateTime? PrcDateIssued { get; set; }

        //[Display(Name = "Membership Type")]
        //public EnumData.MembershipType TypeOfMembership { get; set; }

        //[Display(Name = "Membership #")]
        //public string MembershipNum { get; set; }

        //[Display(Name = "Date of Membership")]
        //public DateTime? DateOfMembership { get; set; }


        //// 
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }

        //[Display(Name = "Middle Name")]
        //public string MiddleName { get; set; }

        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }

        //[Display(Name = "Place of Birth")]
        //public string PlaceOfBirth { get; set; }

        //[Display(Name = "Birthdate")]
        //public DateTime? BirthDate { get; set; }

        //[Display(Name = "Home Address")]
        //public string HomeAddress { get; set; }

        //[Display(Name = "Baguio Address")]
        //public string BaguioAddress { get; set; }

        //[Display(Name = "Telephone Number")]
        //public string TelNum { get; set; }

        //[Display(Name = "Cellphone Number")]
        //public string CellNum { get; set; }

        //[Display(Name = "Email")]
        //public string Email { get; set; }


        //// 
        //[Display(Name = "Employment Sector")]
        //public EnumData.EmploymentSector? EmploymentSector { get; set; }

        //[Display(Name = "Other")]
        //public string OtherEmploymentSector { get; set; }

        //[Display(Name = "Area of Practice")]
        //public EnumData.AreaOfPractice? AreaOfPractice { get; set; }

        //[Display(Name = "Other")]
        //public string OtherAreaOfPractice { get; set; }

        //[Display(Name = "Current Company")]
        //public string CurrentCompany { get; set; }

        //[Display(Name = "Employee Type")]
        //public EnumData.EmployeeType? EmployeeType { get; set; }

        //[Display(Name = "Current Job Position")]
        //public string CurrentJobPosition { get; set; }

        //[Display(Name = "Company/Work Address")]
        //public string WorkAddress { get; set; }

        //[Display(Name = "Company Telephone #")]
        //public string CompanyTelNum { get; set; }

        //[Display(Name = "Company Tele-fax")]
        //public string CompanyFaxNum { get; set; }

        //[Display(Name = "Company Email")]
        //public string CompanyEmail { get; set; }
    }

    // TODO: Changed some properties to enum
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [Display(Name = "Course/Degree")]
        public string Course { get; set; }

        [Display(Name = "School")]
        public string School { get; set; }

        [Display(Name = "Year Graduated")]
        public int? YearGraduated { get; set; }

        [ForeignKey("User")]
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    // TODO: Changed some properties to enum
    public class MembershipInfo
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        [Display(Name = "PRC #")]
        public string PrcNum { get; set; }

        [Display(Name = "PRC Date Issued")]
        public DateTime? PrcDateIssued { get; set; }

        [Display(Name = "Membership Type")]
        public EnumData.MembershipType TypeOfMembership { get; set; }

        [Display(Name = "Membership #")]
        public string MembershipNum { get; set; }

        [Display(Name = "Date of Membership")]
        public DateTime? DateOfMembership { get; set; }


        public virtual ApplicationUser User { get; set; }
    }

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