using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;
using static TestDAL.TestsHelper;

using TestDAL;

namespace TestProject.Tests.DAL
{
    public class CompaniesTests
    {
        DbContext DBContext;
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

            // Act
            DBCompanies.Add(company);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_InvalidData_ThrowsException()
        {
            // Arrange
            var company = CreateTestCompany(GetLongString());

            // Act 
            DBCompanies.Add(company);

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
        public void Modify_InvalidData_ThrowsException()
        {
            // Arrange
            var company = CreateTestCompany();

            // Act
            DBCompanies.Add(company);
            DBContext.SaveChanges();
            company.Name = GetLongString();

            // Assert
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
        }
    }
}
