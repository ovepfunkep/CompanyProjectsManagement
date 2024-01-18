using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace TestDAL
{
    public class TestDbContext : CompaniesProjectsContext
    {
        public TestDbContext()
            : base(@"Server=DESKTOP-RO7HB57;Database=CPMTest;Trusted_Connection=True;")
        { }
    }
}
