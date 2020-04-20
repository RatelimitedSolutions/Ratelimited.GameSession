using Ratelimited.GameSession.Domain;
using Ratelimited.GameSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Database
{
    public static class UserExpression
    {
        public static Expression<Func<User, long>> AuthId => user => user.Auth.Id;

        public static Expression<Func<User, UserModel>> Model => user => new UserModel
        {
            Id = user.Id,
            Name = user.FullName.Name,
            Surname = user.FullName.Surname,
            Email = user.Email.Value
        };

        public static Expression<Func<User, bool>> Id(long id)
        {
            return user => user.Id == id;
        }
    }
}
