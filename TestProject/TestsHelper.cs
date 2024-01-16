using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using DataAccess.Models;

namespace TestDAL
{
    internal static class TestsHelper
    {
        public static Project CreateTestProject(string? name = null, Employee ? givenEmployee = null, Company? givenCompany = null)
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

        public static Employee CreateTestEmployee(string? name = null) => new()
        {
            Name = name ?? "TestEmployee",
            Surname = "Testovich"
        };

        public static Company CreateTestCompany(string? name = null) => new() { Name = name ?? "TestCompany" };

        public static string GetLongString(int howLong = 101, string? text = null)
        {
            string Text = text ?? "TestString";
            return string.Concat(Enumerable.Repeat(Text, 
                                                   Math.Abs(howLong) / Text.Length)) 
                   + Text[..(howLong % Text.Length)];
        }
    }
}
