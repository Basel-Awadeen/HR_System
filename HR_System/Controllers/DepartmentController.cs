using Core.Featuers.Company.Command.Queries;
using Core.Featuers.Department.Command.Models;
using Core.Featuers.Department.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator mediator;
        public DepartmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add_Departments")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> AddDepartments([FromBody] AddNewDepartments command)
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


        [HttpDelete("Delete_Department")]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> DeleteDepartment([FromBody] DeleteDepartment command)
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


        [HttpPost("Add_Employee_ToDepartment")]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployee command)
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

        [HttpPost("Set_Manager_Department")]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> SetManager([FromBody] SetManager command)
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
