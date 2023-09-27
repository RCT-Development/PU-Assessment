using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PU.DataAccess
{
    public class PURepositoryContextFactory : IDesignTimeDbContextFactory<PURepositoryContext>
    {
        public PURepositoryContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb; Database=LocalAssessmentDb; Trusted_Connection=True;";

            if (args.Length > 0)
            {
                connectionString = args[0];
            }

            var contextOptions = new DbContextOptionsBuilder<PURepositoryContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new PURepositoryContext(contextOptions);
        }
    }
}
