using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using LinkedInEmailFinder.Models.UserFields;
using Microsoft.EntityFrameworkCore;

namespace LinkedEmailFinder.DataAccess
{
    public partial class LinkedInEmailFinder_DBContext : IdentityDbContext
    {
        public DbSet<Subscriptions> Subscriptions { get; set; }
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
