using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Database
{
    public static class AuthExpression
    {
        public static Expression<Func<Auth, bool>> Login(string login)
        {
            return auth => auth.Login == login;
        }
    }
}
