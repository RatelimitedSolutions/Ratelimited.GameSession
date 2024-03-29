﻿using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Application
{
    public sealed class FileService : IFileService
    {
        public Task<IEnumerable<BinaryFile>> AddAsync(string directory, IEnumerable<BinaryFile> files)
        {
            return files.SaveAsync(directory);
        }

        public Task<BinaryFile> GetAsync(string directory, Guid id)
        {
            return BinaryFile.ReadAsync(directory, id);
        }
    }
}
