using DotNetCore.Objects;
using DotNetCore.Results;
using Ratelimited.GameSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Application
{
    public interface IUserService
    {
        Task<IResult<long>> AddAsync(UserModel model);

        Task<IResult> DeleteAsync(long id);

        Task<UserModel> GetAsync(long id);

        Task InactivateAsync(long id);

        Task<PagedList<UserModel>> ListAsync(PagedListParameters parameters);

        Task<IEnumerable<UserModel>> ListAsync();

        Task<IResult> UpdateAsync(UserModel model);
    }
}
