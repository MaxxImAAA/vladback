using API.Dtos;
using API.IServices;
using API.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAuthService _userservice;
        public UserController(IUserAuthService _userservice)
        {
            this._userservice = _userservice;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterForm regform)
        {
            await _userservice.Register(regform);
            return Ok();
        }

        [HttpGet("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(string email, string password)
        {
            var request = await _userservice.Login(email, password);
            return Ok(request);
        }
    }
}
