namespace PICECAR.Migrations.ApplicationDbContext
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Config01 : DbMigrationsConfiguration<PICECAR.Models.ApplicationDbContext>
    {
        public Config01()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations\ApplicationDbContext";
        }

        protected override void Seed(PICECAR.Models.ApplicationDbContext context)
        {
            string[] roles =
            {
                "Administrator",
                "Secretary",
                "User"
            };
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
            IdentityRole identityRole;

            // Add a default roles
            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    identityRole = new IdentityRole { Name = role };

                    roleManager.Create(identityRole);
                }
            }

            // Adds a default account
            string[,] users =
            {
                { "admin@email.com", "Admin@123", "Administrator" },
                { "secretary@email.com", "Admin@123", "Secretary" },
                { "user@email.com", "Admin@123", "User" }
            };
            string email;

            for (var i = 0; i < users.Length / 3; i++)
            {
                email = users[i, 0];

                // Add the account if it does not exist
                if (!context.Users.Any(u => u.Email == email))
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var manager = new UserManager<ApplicationUser>(store);
                    var user = new ApplicationUser
                    {
                        Email = email,
                        UserName = email
                    };

                    manager.Create(user, users[i, 1]);
                    manager.AddToRole(user.Id, users[i, 2]);

                    var pInfo = new PersonalInfo
                    {
                        Id = user.Id,
                        FirstName = "First",
                        MiddleName = "Middle",
                        LastName = "Last"
                    };

                    context.PersonalInfos.AddOrUpdate(pInfo);
                    context.SaveChanges();
                }
            }
        }
    }
}
