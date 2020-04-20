using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Ratelimited.GameSession.Database
{
    public class ContextFactory: IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Context>();

            builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Database;");

            return new Context(builder.Options);
        }
    }
}
