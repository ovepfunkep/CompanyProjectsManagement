using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using DataAccess.Models;
using static TestDAL.TestsHelper;

using System.Configuration;
using System.Data.Entity.Migrations;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;

namespace TestDAL.Tests
{
    public class ProjectsTests
    {
        DbContext DBContext;
        DbSet<Project> DBProjects;
        DbSet<Employee> DBEmployees;
        DbSet<Company> DBCompanies;

        [OneTimeSetUp] 
        public void OneTimeSetUp() 
        {
            DBContext = new TestDbContext();

            DBContext.Database.Delete();
            DBContext.Database.Create();

            DBContext.Dispose();
        }

        [SetUp] 
        public void Setup()
        {
            DBContext = new TestDbContext();

            DBProjects = DBContext.Set<Project>();
            DBEmployees = DBContext.Set<Employee>();
            DBCompanies = DBContext.Set<Company>();

            DBContext.Database.BeginTransaction();
        }

        [TearDown] 
        public void TearDown()
        {
            DBContext.Database.CurrentTransaction.Rollback();
            DBContext.Dispose();
        }

        [Test] 
        public void Add_ValidData_DoesntThrowException()
        {
            // Arrange
            var employee = CreateTestEmployee();
            var company = CreateTestCompany();
            var project = CreateTestProject(givenEmployee: employee, givenCompany: company);

            // Act
            DBEmployees.Add(employee);
            DBCompanies.Add(company);
            DBProjects.Add(project);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Add_InvalidData_ThrowsException()
        {
            // Arrange
            var employee = CreateTestEmployee();
            var company = CreateTestCompany();
            var project = CreateTestProject(GetLongString(), employee, company);

            // Act
            DBEmployees.Add(employee);
            DBCompanies.Add(company);
            DBProjects.Add(project);

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Modify_ValidData_DoesntThrowException()
        {
            // Arrange
            var employee = CreateTestEmployee();
            var company = CreateTestCompany();
            var project = CreateTestProject(givenEmployee: employee, givenCompany: company);

            // Act
            DBEmployees.Add(employee);
            DBCompanies.Add(company);
            DBProjects.Add(project);
            DBContext.SaveChanges();
            project.Name = "NewProjectName";

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Modify_InvalidData_ThrowsException()
        {
            // Arrange
            var employee = CreateTestEmployee();
            var company = CreateTestCompany();
            var project = CreateTestProject(givenEmployee: employee, givenCompany: company);

            // Act
            DBEmployees.Add(employee);
            DBCompanies.Add(company);
            DBProjects.Add(project);
            DBContext.SaveChanges();
            project.Name = GetLongString();

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Delete_DoesntThrowException()
        {
            // Arrange
            var employee = CreateTestEmployee();
            var company = CreateTestCompany();
            var project = CreateTestProject(givenEmployee: employee, givenCompany: company);

            // Act
            DBEmployees.Add(employee);
            DBCompanies.Add(company);
            DBProjects.Add(project);
            DBContext.SaveChanges();
            DBProjects.Remove(project);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }
    }
}
