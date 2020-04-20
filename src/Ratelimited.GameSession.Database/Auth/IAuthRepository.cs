using DotNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Database
{
    public interface IAuthRepository : IRepository<Auth>
    {
        Task<bool> AnyByLoginAsync(string login);
        Task<Auth> GetByLoginAsync(string login);
    }
}
