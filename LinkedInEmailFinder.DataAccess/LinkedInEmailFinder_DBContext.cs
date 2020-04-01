using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LinkedInEmailFinder.Models;
using LinkedInEmailFinder.Models.ViewModels;
using LinkedInEmailFinder.Models.UserFields;

namespace LinkedEmailFinder.DataAccess
{
    public partial class LinkedInEmailFinder_DBContext : IdentityDbContext
    {
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=LinkedInEmailFinder_DB;Trusted_Connection=True;");
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
