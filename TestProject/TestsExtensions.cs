using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using DataAccess.Models;

namespace TestDAL
{
    internal static class TestsExtensions
    {
        internal static Project CreateTestProject(string? name = null, Employee ? givenEmployee = null, Company? givenCompany = null)
        {
            var employee = givenEmployee ?? CreateTestEmployee();
            var company = givenCompany ?? CreateTestCompany();
            return new Project()
                       {
                           Name = name ?? "TestProject",
                           ManagerID = employee.ID,
                           ContractorCompanyID = company.ID,
                           CustomerCompanyID = company.ID
                       };
        }

        internal static Employee CreateTestEmployee(string? name = null) => new()
        {
            Name = name ?? "TestEmployee",
            Surname = "Testovich"
        };

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
