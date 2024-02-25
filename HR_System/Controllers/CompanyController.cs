using Core.Featuers.Company.Command.Queries;
using Core.Featuers.Company.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator mediator;
        public CompanyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> CreateCompany([FromForm] CreateCompanyCommand command) 
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

        [HttpPost("DeleteCompany")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> DeleteCompany([FromForm] DeleteCompany command)
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


        [HttpPost("Set_New_HR")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetNEW([FromBody] NewHr command)
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
        [HttpPost("GetAllEmployees")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> GetEmployees([FromBody] View_Employee command)
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

        [HttpPost("Set_Company_Name")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> SetCompanyName([FromBody] SetNewNameForCompany command)
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
