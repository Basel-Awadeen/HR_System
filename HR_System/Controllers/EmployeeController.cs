using Core.Featuers.Account.Command.Models;
using Core.Featuers.Attendace.Command.Model;
using Core.Featuers.Attendace.Queries.Models;
using Core.Featuers.Employee.Command.Model;
using Core.Featuers.Vacation.Command.Models;
using Core.Featuers.Vacation.Queries.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;
        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("RequestVacation")]
        public async Task<IActionResult> Request_Vacation([FromBody] RequestVacationCommand command)
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

        [HttpPost("Update_Vacation")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Update_Vacation([FromBody] UpdateVacation command)
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

        [HttpPost("GetVacations")]
        public async Task<IActionResult> GetVacations([FromBody] GetAllVacations command)
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

        [Authorize]
        [HttpPost("Sign_Attend")]
        public async Task<IActionResult> Attend([FromForm] SignAttendance command)
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
        [Authorize]
        [HttpPost("Get_Attendance")]
        public async Task<IActionResult> GetAttendance([FromForm] GetAttendanceEmployee command)
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
