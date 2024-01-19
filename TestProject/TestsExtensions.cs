using System.Data.Entity;

using DataAccess.Models;

namespace TestDAL
{
    internal static class TestsExtensions
    {
        internal static Project CreateTestProject(string? name = null, 
                                                  DateTime? dateStarted = null,
                                                  DateTime? dateEnded = null,
                                                  int? priority = null,
                                                  Employee? givenManager = null, 
                                                  Company? givenCustomer = null, 
                                                  ICollection<Employee>? employees = null)
        {
            var manager = givenManager ?? CreateTestEmployee();
            var contractor = manager.Company;
            var customer = givenCustomer ?? CreateTestCompany("CustomerCompany");
            return new Project()
            {
                Name = name ?? "TestProject",
                DateStarted = dateStarted ?? DateTime.Now,
                DateEnded = dateEnded ?? DateTime.Now,
                Priority = priority ?? 0,
                ManagerID = -1,
                Manager = manager,
                CustomerCompanyID = -1,
                ContractorCompanyID = -1,
                CustomerCompany = customer,
                ContractorCompany = contractor,
                Employees = employees ?? new HashSet<Employee>() { manager, CreateTestEmployee("TestEmployee1", "Testov1", givenCompany: contractor) }
            };
        }

        internal static Employee CreateTestEmployee(string? name = null,
                                                    string? surname = null,
                                                    string? patronymic = null,
                                                    string? email = null,
                                                    Company? givenCompany = null)
        {
            var company = givenCompany ?? CreateTestCompany();
            return new()
            {
                Name = name ?? "TestEmployee",
                Surname = surname ?? "Testov",
                Company = company,
                CompanyID = company.ID,
                Patronymic = patronymic ?? "Testovich",
                Email = email ?? "Test@mail.ru"
            };
        }

        internal static Company CreateTestCompany(string? name = null) => new() { Name = name ?? "TestCompany" };

        internal static string GetLongString(int howLong = 101, string? text = null)
        {
            string Text = text ?? "TestString";
            return string.Concat(Enumerable.Repeat(Text,
                                                   Math.Abs(howLong) / Text.Length))
                   + Text[..(howLong % Text.Length)];
        }

        internal static (bool Success, string? ErrorMessage) ClearDatabase(DbContext dbContext)
        {
            dbContext.Set<Project>().RemoveRange(dbContext.Set<Project>());
            dbContext.Set<Company>().RemoveRange(dbContext.Set<Company>());
            dbContext.Set<Employee>().RemoveRange(dbContext.Set<Employee>());

            try
            {
                dbContext.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
