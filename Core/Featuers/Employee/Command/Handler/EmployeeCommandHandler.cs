using AutoMapper;
using Core.Bases;
using Core.Featuers.Company.Command.Queries;
using Core.Featuers.Employee.Command.Model;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Employee.Command.Handler
{
    public class EmployeeCommandHandler : ResponseHandler,
        IRequestHandler<RequestVacationCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;
        #endregion

        #region Constructor
        public EmployeeCommandHandler(IMapper mapper, IEmployeeService employeeService)
        {
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<string>> Handle(RequestVacationCommand request, CancellationToken cancellationToken)
        {
            var result = await employeeService.RequestVacation(request.Employee_Email, request.Reason, request.Day_Number);
            if (result == "Vaction Requested is Successfully") return Success(result);
            return BadRequest<string>(result);
        }
        #endregion
    }
}
