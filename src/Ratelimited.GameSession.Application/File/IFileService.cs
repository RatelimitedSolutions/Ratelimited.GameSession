using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Application
{
    public interface IFileService
    {
        Task<IEnumerable<BinaryFile>> AddAsync(string directory, IEnumerable<BinaryFile> files);

        Task<BinaryFile> GetAsync(string directory, Guid id);
    }
}
