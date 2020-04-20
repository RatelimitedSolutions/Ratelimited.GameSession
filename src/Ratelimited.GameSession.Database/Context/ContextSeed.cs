using Microsoft.EntityFrameworkCore;
using Ratelimited.GameSession.Domain;

namespace Ratelimited.GameSession.Database
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedAuths();
            builder.SeedUsers();
        }

        private static void SeedAuths(this ModelBuilder builder)
        {
            builder.Entity<Auth>(auth =>
            {
                auth.HasData(new
                {
                    Id = 1L,
                    Login = "admin",
                    Password = "Mai9hoshaiTei5eiEefuth4eTa2eiphuohTei6er",
                    Salt = "876BC27F-E2E2-4F2F-8436-4F8BD4890705",
                    Roles = Roles.User | Roles.Admin
                });
            });
        }

        private static void SeedUsers(this ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasData(new
                {
                    Id = 1L,
                    Status = Status.Active,
                    AuthId = 1L
                });

                user.OwnsOne(owned => owned.FullName).HasData(new
                {
                    UserId = 1L,
                    Name = "Administrator",
                    Surname = "Administrator"
                });

                user.OwnsOne(owned => owned.Email).HasData(new
                {
                    UserId = 1L,
                    Value = "administrator@administrator.com"
                });
            });
        }

    }
}
