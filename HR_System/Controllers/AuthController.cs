using Core.Featuers.Auth.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Verfiy_OTP")]
        [AllowAnonymous]
        public async Task<IActionResult> Verfiy_OTP([FromBody] Verfiy_OTP command)
        {
            try
            {
                var response = await mediator.Send(command);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] SignIn command)
        {
            try
            {
                var response = await mediator.Send(command);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
