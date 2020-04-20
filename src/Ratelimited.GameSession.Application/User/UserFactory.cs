using Ratelimited.GameSession.Domain;
using Ratelimited.GameSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Application
{
    public static class UserFactory
    {
        public static User Create(UserModel model,Auth auth)
        {
            return new User
            (
                new FullName(model.Name, model.Surname),
                new Email(model.Email),
                auth
            );
        }
    }
}
