using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PICECAR.Extension
{
    public class EnumData
    {
        public enum MembershipType
        {
            [Display(Name = "Regular Member")]
            Regular,

            [Display(Name = "Life Member")]
            Life,

            [Display(Name = "Student Member")]
            Student,

            [Display(Name = "Honorary Member")]
            Honorary,

            [Display(Name = "Fellow")]
            Fellow,
        }
    }
}