using System.Data.Entity;
using System.Data.Entity.Validation;

using DataAccess.Models;

using TestDAL;

using static TestDAL.TestsExtensions;

namespace TestProject.Tests.DALTests
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

            var (success, errorMessage) = ClearDatabase(DBContext);

            if (!success) Assert.Fail($"Database clearing failed. Error: {errorMessage}");

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
            var customerCompany = CreateTestCompany("CustomerCompany");
            var project = CreateTestProject(givenCustomer: customerCompany);

            // Act
            DBEmployees.AddRange(project.Employees);
            DBCompanies.Add(project.CustomerCompany);
            DBCompanies.Add(project.ContractorCompany);
            DBProjects.Add(project);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_InvalidData_ThrowsException()
        {
            // Arrange
            var project = CreateTestProject(GetLongString());

            // Act
            DBEmployees.AddRange(project.Employees);
            DBCompanies.Add(project.ContractorCompany);
            DBProjects.Add(project);

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ValidData_DoesntThrowException()
        {
            // Arrange
            var project = CreateTestProject();

            // Act
            DBEmployees.AddRange(project.Employees);
            DBCompanies.Add(project.ContractorCompany);
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
            var project = CreateTestProject();

            // Act
            DBEmployees.AddRange(project.Employees);
            DBCompanies.Add(project.ContractorCompany);
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
            var project = CreateTestProject();

            // Act
            DBEmployees.AddRange(project.Employees);
            DBCompanies.Add(project.ContractorCompany);
            DBProjects.Add(project);
            DBContext.SaveChanges();
            DBProjects.Remove(project);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
            Assert.That(DBProjects.Count, Is.EqualTo(0));
        }
    }
}
