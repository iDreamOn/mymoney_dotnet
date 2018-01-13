﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace mymoney.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Spending> Spendings { get; set; }
        public ICollection<SpendingCategory> SpendingCategories { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<FinancialGoal> FinancialGoals { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<mymoney.Models.Spending> Spendings { get; set; }

        public System.Data.Entity.DbSet<mymoney.Models.SpendingCategory> SpendingCategories { get; set; }

        public System.Data.Entity.DbSet<mymoney.Models.Budget> Budgets { get; set; }

        public System.Data.Entity.DbSet<mymoney.Models.FinancialGoal> FinancialGoals { get; set; }
    }
}