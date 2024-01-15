using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace DataAccess
{
    public class CompaniesProjectsContext : DbContext
    {
        private static readonly string DefaultConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=CompaniesProjects;";

        public int ID { get; set; }
        public required DbSet<Company> Companies { get; set; }
        public required DbSet<Employee> Employees { get; set; }
        public required DbSet<Project> Projects { get; set; }

        public CompaniesProjectsContext(string connectionString) : base(connectionString) { }

        public CompaniesProjectsContext() : base(DefaultConnectionString) { }
    }
}
