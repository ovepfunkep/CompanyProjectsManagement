using System.Data.Entity;
using System.Data.Entity.Validation;

using DataAccess.Models;

using TestDAL;

using static TestDAL.TestsExtensions;

namespace TestProject.Tests.DALTests
{
    public class EmployeesTests
    {
        DbContext DBContext;
        DbSet<Employee> DBEmployees;

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
        public void Add_LongNameEmployee_ThrowsException()
        {
            // Arrange
            var longNameEmployee = CreateTestEmployee(GetLongString());

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(longNameEmployee); DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_LongSurnameEmployee_ThrowsException()
        {
            // Arrange
            var longSurnameEmployee = CreateTestEmployee(surname: GetLongString());

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(longSurnameEmployee); DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_LongPatronymicEmployee_ThrowsException()
        {
            // Arrange
            var longPatronymicEmployee = CreateTestEmployee(patronymic: GetLongString());

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(longPatronymicEmployee); DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_LongEmailEmployee_ThrowsException()
        {
            // Arrange
            var longEmailEmployee = CreateTestEmployee(email: GetLongString());

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(longEmailEmployee); DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_ShortNameEmployee_ThrowsException()
        {
            // Arrange
            var shortNameEmployee = CreateTestEmployee("");

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(shortNameEmployee); DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_ShortSurnameEmployee_ThrowsException()
        {
            // Arrange
            var shortSurnameEmployee = CreateTestEmployee(surname: "");

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(shortSurnameEmployee); DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_ShortPatronymicEmployee_ThrowsException()
        {
            // Arrange
            var shortPatronymicEmployee = CreateTestEmployee(patronymic: "");

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(shortPatronymicEmployee); DBContext.SaveChanges(); });
        }

        [Test]
        public void Add_ShortEmailEmployee_ThrowsException()
        {
            // Arrange
            var shortEmailEmployee = CreateTestEmployee(email: "");

            // Act and Assert
            Assert.Throws<DbEntityValidationException>(() => { DBEmployees.Add(shortEmailEmployee); DBContext.SaveChanges(); });
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
        public void Modify_LongNameEmployee_ThrowsException()
        {
            // Arrange
            var longNameEmployee = CreateTestEmployee();
            DBEmployees.Add(longNameEmployee);
            DBContext.SaveChanges();

            // Act
            longNameEmployee.Name = GetLongString();

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_LongSurnameEmployee_ThrowsException()
        {
            // Arrange
            var longSurnameEmployee = CreateTestEmployee();
            DBEmployees.Add(longSurnameEmployee);
            DBContext.SaveChanges();

            // Act
            longSurnameEmployee.Surname = GetLongString();

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_LongPatronymicEmployee_ThrowsException()
        {
            // Arrange
            var longPatronymicEmployee = CreateTestEmployee();
            DBEmployees.Add(longPatronymicEmployee);
            DBContext.SaveChanges();

            // Act
            longPatronymicEmployee.Patronymic = GetLongString();

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_LongEmailEmployee_ThrowsException()
        {
            // Arrange
            var longEmailEmployee = CreateTestEmployee();
            DBEmployees.Add(longEmailEmployee);
            DBContext.SaveChanges();

            // Act
            longEmailEmployee.Email = GetLongString();

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ShortNameEmployee_ThrowsException()
        {
            // Arrange
            var shortNameEmployee = CreateTestEmployee();
            DBEmployees.Add(shortNameEmployee);
            DBContext.SaveChanges();

            // Act
            shortNameEmployee.Name = "";

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ShortSurnameEmployee_ThrowsException()
        {
            // Arrange
            var shortSurnameEmployee = CreateTestEmployee();
            DBEmployees.Add(shortSurnameEmployee);
            DBContext.SaveChanges();

            // Act
            shortSurnameEmployee.Surname = "";

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ShortPatronymicEmployee_ThrowsException()
        {
            // Arrange
            var shortPatronymicEmployee = CreateTestEmployee();
            DBEmployees.Add(shortPatronymicEmployee);
            DBContext.SaveChanges();

            // Act
            shortPatronymicEmployee.Patronymic = "";

            // Assert
            Assert.Throws<DbEntityValidationException>(() => { DBContext.SaveChanges(); });
        }

        [Test]
        public void Modify_ShortEmailEmployee_ThrowsException()
        {
            // Arrange
            var shortEmailEmployee = CreateTestEmployee();
            DBEmployees.Add(shortEmailEmployee);
            DBContext.SaveChanges();

            // Act
            shortEmailEmployee.Email = "";

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
            Assert.That(DBEmployees.Count, Is.EqualTo(0));
        }
    }
}
