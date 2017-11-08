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
            Regular = 1,

            [Display(Name = "Life Member")]
            Life = 2,

            [Display(Name = "Student Member")]
            Student = 3,

            [Display(Name = "Honorary Member")]
            Honorary = 4,

            [Display(Name = "Fellow")]
            Fellow = 5
        }

        public enum EmploymentSector
        {
            [Display(Name = "Government")]
            Government = 1,

            [Display(Name = "Private")]
            Private = 2,

            [Display(Name = "OCW")]
            OCW = 3,

            [Display(Name = "Others")]
            Others = 100,
        }

        public enum AreaOfPractice
        {
            [Display(Name = "Construction")]
            Construction = 1,

            [Display(Name = "Design")]
            Design = 2,

            [Display(Name = "Academy")]
            Academy = 3,

            [Display(Name = "Commercial")]
            Commercial = 4,

            [Display(Name = "Others")]
            Others = 100,
        }

        public enum EmployeeType
        {
            [Display(Name = "Others")]
            Others = 100,
        }
    }
}