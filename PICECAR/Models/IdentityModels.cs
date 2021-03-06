﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System;

namespace PICECAR.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public DateTime LastActive { get; set; }

        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual MembershipInfo MembershipInfo { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual IList<Education> Educations { get; set; }

        public virtual IList<PaymentOfDues> PaymentOfDue { get; set; }
        public virtual IList<Seminar> Seminar { get; set; }
        public virtual MembershipStatus MembershipStatuses { get; set; }
        public virtual Chapter Chapters { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<MembershipInfo> MembershipInfos { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Education> Educations { get; set; }

        public DbSet<PaymentOfDues> PaymentOfDues { get; set; }
        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<MembershipStatus> MembershipStatuses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        public ApplicationDbContext()
            : base("PICEDatabase", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IdentityUser>()
                .ToTable("User");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");

            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("UserRole");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("UserLogin");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");
        }
    }
}