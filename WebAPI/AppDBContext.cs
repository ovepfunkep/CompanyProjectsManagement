using DataAccess;

namespace WebAPI
{
    public class AppDbContext : CompaniesProjectsContext
    {
        public AppDbContext() : base("Server=host.docker.internal;Database=CPMMaster;User=DataManager;Password=HardPassword;Connection Timeout=5;") { }
    }
}
