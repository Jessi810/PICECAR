using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    // TODO: Changed some properties to enum
    public class MembershipInfo
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        public string PrcNum { get; set; }

        public DateTime? PrcDateIssued { get; set; }

        public string TypeOfMembership { get; set; }

        public string MembershipNum { get; set; }

        public DateTime? DateOfMembership { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}