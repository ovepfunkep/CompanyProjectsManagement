using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using DataAccess.Models;

namespace DataAccess
{
    public class CompaniesProjectsContext : DbContext
    {
        private static readonly string DefaultConnectionString = @"Server=DESKTOP-RO7HB57;Database=CPMMaster;Trusted_Connection=True;";

        public DbSet<Company>? Companies { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Project>? Projects { get; set; }

        public CompaniesProjectsContext(string connectionString) : base(connectionString) { }

        public CompaniesProjectsContext() : base(DefaultConnectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                        .HasRequired(p => p.Manager)
                        .WithMany(m => m.Projects)
                        .HasForeignKey(p => p.ManagerID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                        .HasRequired(p => p.CustomerCompany)
                        .WithMany(cc => cc.OrderedProjects)
                        .HasForeignKey(p => p.CustomerCompanyID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                        .HasRequired(p => p.ContractorCompany)
                        .WithMany(cc => cc.MadenProjects)
                        .HasForeignKey(p => p.ContractorCompanyID)
                        .WillCascadeOnDelete(false);
        }
    }
}
