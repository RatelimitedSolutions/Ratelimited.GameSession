using DotNetCore.AspNetCore;
using DotNetCore.Objects;
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
    [Route("Users")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public Task<IActionResult> AddAsync(UserModel model)
        {
            return _userService.AddAsync(model).ResultAsync();
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteAsync(long id)
        {
            return _userService.DeleteAsync(id).ResultAsync();
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetAsync(long id)
        {
            return _userService.GetAsync(id).ResultAsync();
        }

        [HttpPatch("{id}/Inactivate")]
        public Task InactivateAsync(long id)
        {
            return _userService.InactivateAsync(id);
        }

        [HttpGet("List")]
        public Task<IActionResult> ListAsync([FromQuery]PagedListParameters parameters)
        {
            return _userService.ListAsync(parameters).ResultAsync();
        }

        [HttpGet]
        public Task<IActionResult> ListAsync()
        {
            return _userService.ListAsync().ResultAsync();
        }

        [HttpPut("{id}")]
        public Task<IActionResult> UpdateAsync(UserModel model)
        {
            return _userService.UpdateAsync(model).ResultAsync();
        }
    }
}
