using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    public class Transact
    {
        public class PaymentOfDues
        {
            [Key]
            public string PaymentOfDuesId { get; set; }

            public int? InclusiveYearFrom { get; set; }

            public int? InclusiveYearTo { get; set; }

            public DateTime? LifeMemberPayment { get; set; }

            public DateTime? PaymentDate { get; set; }

            public decimal PaymentAmount { get; set; }

            public string OrNum { get; set; }

            [ForeignKey("User")]
            public string Id { get; set; }
            public virtual ApplicationUser User { get; set; }
        }

        public class Seminar
        {
            [Key]
            public string SeminarId { get; set; }

            public string Code { get; set; }

            public string Title { get; set; }

            public string Topic { get; set; }

            public DateTime? DateFrom { get; set; }

            public DateTime? DateTo { get; set; }

            public int? Hours { get; set; }

            public int? CpdUnitsEarned { get; set; }

            [ForeignKey("User")]
            public string Id { get; set; }
            public virtual ApplicationUser User { get; set; }
        }

        public class MembershipStatus
        {
            [Key, ForeignKey("User")]
            public string Id { get; set; }

            public string CurrentStatus { get; set; }

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
}