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
        public int? InclusiveYearFrom { get; set; }

        [Display(Name = "to")]
        public int? InclusiveYearTo { get; set; }

        [Display(Name = "Date")]
        public DateTime? LifeMemberPayment { get; set; }

        [Display(Name = "Date")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Amount")]
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
        public string Title { get; set; }

        [Display(Name = "Topics")]
        public string Topic { get; set; }

        [Display(Name = "Date")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "to")]
        public DateTime? DateTo { get; set; }

        [Display(Name = "# of Hours")]
        public int? Hours { get; set; }

        [Display(Name = "CPD units earned")]
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

        public string CurrentChapter { get; set; }

        public string NewChapter { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}