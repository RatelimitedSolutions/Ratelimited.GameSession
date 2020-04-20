using DotNetCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Database
{
    public sealed class AuthRepository : EFRepository<Auth>, IAuthRepository
    {
        public AuthRepository(Context context): base(context) { }

        public Task<bool> AnyByLoginAsync(string login)
        {
            return Queryable.AnyAsync(AuthExpression.Login(login));
        }

        public Task<Auth> GetByLoginAsync(string login)
        {
            return Queryable.SingleOrDefaultAsync(AuthExpression.Login(login));
        }
    }
}
