using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Infrastructure.Data
{
    public class JobBoardContextFactory : IDesignTimeDbContextFactory<JobBoardContext>
    {
        public JobBoardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JobBoardContext>();
            optionsBuilder.UseSqlServer(
                "Server=LAPTOP-EPP1LDGQ\\MSSQLSERVER04;Database=JobBoardAPI;Trusted_Connection=true;TrustServerCertificate=true;");

            return new JobBoardContext(optionsBuilder.Options);
        }
    }
}
