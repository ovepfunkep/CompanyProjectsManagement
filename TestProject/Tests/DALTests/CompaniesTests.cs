using System.Data.Entity;
using System.Data.Entity.Validation;

using DataAccess.Models;

using TestDAL;

using static TestDAL.TestsExtensions;

namespace TestProject.Tests.DALTests
{
    public class CompaniesTests
    {
        DbContext DBContext;
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
            var company = CreateTestCompany();
            company.Employees.Add(CreateTestEmployee(givenCompany: company));
            company.Employees.Add(CreateTestEmployee(givenCompany: company));

            // Act
            DBCompanies.Add(company);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_LongNameCompany_ThrowsException()
        {
            // Arrange
            var company = CreateTestCompany(GetLongString());

            // Act 
            DBCompanies.Add(company);

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_ShortNameCompany_ThrowsException()
        {
            // Arrange
            var company = CreateTestCompany("");

            // Act 
            DBCompanies.Add(company);

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_CompanyTwice_ThrowsException()
        {
            // Arrange
            var company = CreateTestCompany("");
            var companyCopy = CreateTestCompany(company.Name);

            // Act 
            DBCompanies.Add(company);
            DBCompanies.Add(companyCopy);

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ValidData_DoesntThrowException()
        {
            // Arrange
            var company = CreateTestCompany();

            // Act
            DBCompanies.Add(company);
            DBContext.SaveChanges();
            company.Name = "newName";

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_LongNameCompany_ThrowsException()
        {
            // Arrange
            var company = CreateTestCompany();
            DBCompanies.Add(company);
            DBContext.SaveChanges();

            // Act 
            company.Name = GetLongString();

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ShortNameCompany_ThrowsException()
        {
            // Arrange
            var company = CreateTestCompany();
            DBCompanies.Add(company);
            DBContext.SaveChanges();

            // Act 
            company.Name = "";

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Delete_DoesntThrowException()
        {
            // Arrange
            var company = CreateTestCompany();

            // Act
            DBCompanies.Add(company);
            DBContext.SaveChanges();
            DBCompanies.Remove(company);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
            Assert.That(DBCompanies.Count, Is.EqualTo(0));
        }
    }
}
