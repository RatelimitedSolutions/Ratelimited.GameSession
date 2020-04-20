using DotNetCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ratelimited.GameSession.Domain;
using Ratelimited.GameSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Database
{
    public sealed class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }

        public Task<long> GetAuthIdByUserIdAsync(long id)
        {
            return Queryable.Where(UserExpression.Id(id)).Select(UserExpression.AuthId).SingleOrDefaultAsync();
        }

        public Task<UserModel> GetByIdAsync(long id)
        {
            return Queryable.Where(UserExpression.Id(id)).Select(UserExpression.Model).SingleOrDefaultAsync();
        }

        public Task UpdateStatusAsync(User user)
        {
            return UpdatePartialAsync(user.Id, new { user.Status });
        }
    }
}
