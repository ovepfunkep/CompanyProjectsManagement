using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace TestProject.Tests.DAL
{
    public class EmployeeTests
    {
        DbContext _dbContext;
        static readonly string TooLongString = string.Concat(Enumerable.Repeat("Test", 26)); // This equals to 26 * 4 = 104

        [SetUp]
        public void Setup()
        {
            _dbContext = new DataAccess.CompaniesProjectsContext();
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public void Add_ValidData_DoesntThrowException()
        {
            // Arrange
            var name = "Test";
            var surname = "Testovich";
            var employee = new Employee()
            {
                Name = name,
                Surname = surname
            };

            // Act
            _dbContext.Set<Employee>().Add(employee);

            // Assert
            Assert.DoesNotThrow(() => { _dbContext.SaveChanges(); });
        }

        [Test]
        public void Add_InvalidData_ThrowsException()
        {
            // Arrange
            var employee = new Employee()
            {
                Name = TooLongString,    // Limit is 100 symbols
                Surname = TooLongString, 
            };

            // Act 
            _dbContext.Set<Employee>().Add(employee);

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { _dbContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ValidData_DoesntThrowException()
        {
            // Arrange
            var name = "Test";
            var surname = "Testovich";
            var newName = "newTest";
            var newSurname = "newTestovich";
            var employee = new Employee()
            {
                Name = name,
                Surname = surname
            };

            // Act
            _dbContext.Set<Employee>().Add(employee);
            _dbContext.SaveChanges();
            employee.Name = newName;
            employee.Surname = newSurname;

            // Assert
            Assert.DoesNotThrow(() => { _dbContext.SaveChanges(); });
        }

        [Test]
        public void Modify_InvalidData_ThrowsException()
        {
            // Arrange
            var name = "Test";
            var surname = "Testovich";
            var newName = TooLongString;
            var newSurname = TooLongString;
            var employee = new Employee()
            {
                Name = name,
                Surname = surname
            };

            // Act
            _dbContext.Set<Employee>().Add(employee);
            _dbContext.SaveChanges();
            employee.Name = newName;
            employee.Surname = newSurname;

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { _dbContext.SaveChanges(); });
        }

        [Test]
        public void Delete_DoesntThrowException()
        {
            // Arrange
            var name = "Test";
            var surname = "Testovich";
            var employee = new Employee()
            {
                Name = name,
                Surname = surname
            };

            // Act
            _dbContext.Set<Employee>().Add(employee);
            _dbContext.SaveChanges();
            _dbContext.Set<Employee>().Remove(employee);

            // Assert
            Assert.DoesNotThrow(() => { _dbContext.SaveChanges(); });
        }
    }
}
