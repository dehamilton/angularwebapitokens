using UserExpense.WebApi.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserExpense.WebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "dhamilton",
                Email = "test@test.com",
                EmailConfirmed = true,
                FirstName = "Derek",
                LastName = "Hamilton",
                RegisterDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "P@ssword!");
        }
    }
}
