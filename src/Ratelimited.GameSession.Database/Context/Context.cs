using Microsoft.EntityFrameworkCore;
using DotNetCore.EntityFrameworkCore;

namespace Ratelimited.GameSession.Database
{
    public class Context: DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddConfigurationsFromAssembly();
            builder.Seed();
        }
    }
}
