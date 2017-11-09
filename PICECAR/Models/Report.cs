using PICECAR.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PICECAR.Models
{
    public class ActiveMember
    {
        public DateTime Date { get; set; }

        [Display(Name = "Membership Type")]
        public List<MembershipTypeProp> MembershipTypeItems { get; set; }

        [Display(Name = "Employment Sector")]
        public List<EmploymentSectorProp> EmploymentSectorItems { get; set; }

        [Display(Name = "Area of Practice")]
        public List<AreaOfPracticeProp> AreaOfPracticeItems { get; set; }
    }

    public class MembershipTypeProp
    {
        public EnumData.MembershipType MembershipType { get; set; }

        public bool IsSelected { get; set; }
    }

    public class EmploymentSectorProp
    {
        public EnumData.EmploymentSector EmploymentSector { get; set; }

        public bool IsSelected { get; set; }
    }

    public class AreaOfPracticeProp
    {
        public EnumData.AreaOfPractice AreaOfPractice { get; set; }

        public bool IsSelected { get; set; }
    }
}