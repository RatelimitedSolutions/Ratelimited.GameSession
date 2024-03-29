﻿using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Ratelimited.GameSession.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.UI.Controllers
{
    [ApiController]
    [Route("Files")]
    public class FileController : ControllerBase
    {
        private readonly IContentTypeProvider _contentTypeProvider;

        private readonly string _directory;

        private readonly IFileService _fileService;

        public FileController
        (
            IContentTypeProvider contentTypeProvider,
            IFileService fileService,
            IHostEnvironment environment
        )
        {
            _contentTypeProvider = contentTypeProvider;
            _directory = Path.Combine(environment.ContentRootPath, "Files");
            _fileService = fileService;
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public Task<IEnumerable<BinaryFile>> AddAsync()
        {
            return _fileService.AddAsync(_directory, Request.Files());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var file = await _fileService.GetAsync(_directory, id);

            _contentTypeProvider.TryGetContentType(file.ContentType, out var contentType);

            return File(file.Bytes, contentType, file.Name);
        }
    }
}
