using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LusApi.Model;
using LusCore.User;
using LusService.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LusApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]UserModel user)
        {
            IActionResult response = Unauthorized();
            var userDto = _userService.Login(user);
            if (userDto == null)
                return response;
            var token = _userService.GenerateToken(userDto);
            if (token != null)
            {
                return Ok(new LoginUserModel()
                {
                    Username = userDto.Username,
                    Token = token
                });
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("getInfor")]
        public IActionResult GetInfor()
        {
            var currentUser = HttpContext.User;
            var user = new UserModel { };
            user.Type = currentUser.Claims.FirstOrDefault(c => c.Type == "type").Value;
            user.Username = currentUser.Claims.FirstOrDefault(c => c.Type == "username").Value;
            var test = HttpContext.User;
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]UserModel user)
        {
            if (user == null)
                return BadRequest("Unable to register");
            _userService.Register(user);
            return Ok();
        }
    }
}