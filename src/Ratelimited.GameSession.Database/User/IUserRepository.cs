using Ratelimited.GameSession.Domain;
using Ratelimited.GameSession.Model;
using DotNetCore.Repositories;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Database
{
    public interface IUserRepository : IRepository<User>
    {
        Task<long> GetAuthIdByUserIdAsync(long id);
        Task<UserModel> GetByIdAsync(long id);
        Task UpdateStatusAsync(User user);
    }
}
