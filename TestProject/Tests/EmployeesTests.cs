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
    public class EmployeesTests
    {
        DbContext DBContext;
        DbSet<Employee> DBEmployees;

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

            DBEmployees = DBContext.Set<Employee>();

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

            // Act
            DBEmployees.Add(employee);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Add_InvalidData_ThrowsException()
        {
            // Arrange
            var employee = CreateTestEmployee(GetLongString());

            // Act 
            DBEmployees.Add(employee);

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Modify_ValidData_DoesntThrowException()
        {
            // Arrange
            var employee = CreateTestEmployee();

            // Act
            DBEmployees.Add(employee);
            DBContext.SaveChanges();
            employee.Name = "newName";

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Modify_InvalidData_ThrowsException()
        {
            // Arrange
            var employee = CreateTestEmployee();

            // Act
            DBEmployees.Add(employee);
            DBContext.SaveChanges();
            employee.Name = GetLongString();

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test] 
        public void Delete_DoesntThrowException()
        {
            // Arrange
            var employee = CreateTestEmployee();

            // Act
            DBEmployees.Add(employee);
            DBContext.SaveChanges();
            DBEmployees.Remove(employee);

            // Assert
            Assert.DoesNotThrow(() => { DBContext.SaveChanges(); });
        }
    }
}
