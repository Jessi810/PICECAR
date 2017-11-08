using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    public class PaymentOfDues
    {
        [Key]
        public int PaymentOfDuesId { get; set; }

        [Display(Name = "From")]
        [Range(1900, 3000, ErrorMessage = "Invalid year")]
        public int? InclusiveYearFrom { get; set; }

        [Display(Name = "to")]
        [Range(1900, 3000, ErrorMessage = "Invalid year")]
        public int? InclusiveYearTo { get; set; }

        [Display(Name = "Date")]
        //[DataType(DataType.Date, ErrorMessage = "Invalid date")]
        public DateTime? LifeMemberPayment { get; set; }

        [Display(Name = "Date")]
        //[DataType(DataType.Date, ErrorMessage = "Invalid date")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid amount")]
        public decimal? PaymentAmount { get; set; }

        [Display(Name = "OR #")]
        public string OrNum { get; set; }

        [ForeignKey("User")]
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class Seminar
    {
        [Key]
        public int SeminarId { get; set; }

        [Display(Name = "ID / Code")]
        public string Code { get; set; }

        [Display(Name = "Title")]
        [MaxLength(150, ErrorMessage = "{0} should only be {1} in length")]
        public string Title { get; set; }

        [Display(Name = "Topics")]
        [MaxLength(5000, ErrorMessage = "{0} should only be {1} in length")]
        public string Topic { get; set; }

        [Display(Name = "Date")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "to")]
        public DateTime? DateTo { get; set; }

        [Display(Name = "# of Hours")]
        [Range(1, 1000, ErrorMessage = "Should be {1} to {2} hours")]
        public int? Hours { get; set; }

        [Display(Name = "CPD units earned")]
        [Range(1, 1000, ErrorMessage = "Should be {1} to {2} units")]
        public int? CpdUnitsEarned { get; set; }

        [ForeignKey("User")]
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class MembershipStatus
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        [Display(Name = "Current Status")]
        public string CurrentStatus { get; set; }

        [Display(Name = "New Status")]
        public string NewStatus { get; set; }


        public virtual ApplicationUser User { get; set; }
    }

    public class Chapter
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        [Display(Name = "Current Chapter")]
        public string CurrentChapter { get; set; }

        [Display(Name = "New Chapter")]
        public string NewChapter { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}