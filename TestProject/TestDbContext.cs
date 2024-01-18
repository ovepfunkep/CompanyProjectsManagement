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
