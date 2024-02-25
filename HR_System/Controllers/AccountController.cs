using Core.Featuers.Account.Command.Models;
using Core.Featuers.Company.Command.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO command)
        {
            try
            {
                var response = await mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Internal Server Error");
            }

        }
    }
}
