using Ratelimited.GameSession.Domain;
using Ratelimited.GameSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Application
{
    public static class AuthFactory
    {
        public static Auth Create(AuthModel model)
        {
            return new Auth(model.Login, model.Password, (Roles)model.Roles);
        }
    }
}
