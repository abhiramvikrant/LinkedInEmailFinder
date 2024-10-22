﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using LinkedInEmailFinder.Models.UserFields;
using Microsoft.EntityFrameworkCore;

namespace LinkedEmailFinder.DataAccess
{
    public partial class LinkedInEmailFinder_DBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Countries> Countries { get; set; }

        public DbSet<States> States { get; set; }

        public DbSet<SubscriptionPurchases> SubscriptionPurchases { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<SubscriptionPurchasesListViewModel> SubscriptionPurchasesListViewModel { get; set; }
        public LinkedInEmailFinder_DBContext()
        {
        }

        public LinkedInEmailFinder_DBContext(DbContextOptions<LinkedInEmailFinder_DBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
