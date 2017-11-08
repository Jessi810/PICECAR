using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PICECAR.Extension;

namespace PICECAR.Models
{
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
}