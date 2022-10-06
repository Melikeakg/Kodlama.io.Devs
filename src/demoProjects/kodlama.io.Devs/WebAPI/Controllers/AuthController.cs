using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.RegisterUser;
using Application.Features.Users.Dtos;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand userRegisterCommand)
        {
            UserDto result = await Mediator.Send(userRegisterCommand);
            return Ok(result);   
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            AccessToken result=await Mediator.Send(loginUserCommand);
            return Ok(result);
        }
    }
}
