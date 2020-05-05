using DotNetCore.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ratelimited.GameSession.Application;
using Ratelimited.GameSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.UI.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [Route("signin")]
        [HttpPost]
        public Task<IActionResult> SignInAsync(SignInModel model)
        {
            return _authService.SignInAsync(model).ResultAsync();
        }

    }
}
