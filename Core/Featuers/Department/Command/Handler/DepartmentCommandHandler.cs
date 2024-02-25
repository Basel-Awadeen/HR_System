using AutoMapper;
using Core.Bases;
using Core.Featuers.Company.Command.Queries;
using Core.Featuers.Department.Command.Models;
using Domain.Entities;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Department.Command.Handler
{
    public class DepartmentCommandHandler : ResponseHandler,
        IRequestHandler<AddNewDepartments, Response<string>>
      , IRequestHandler<AddEmployee, Response<string>>
      , IRequestHandler<SetManager, Response<string>>

    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IDepartmentService departmentService;
        #endregion

        #region Constructor
        public DepartmentCommandHandler(IMapper mapper, IDepartmentService departmentService)
        {
            this.mapper = mapper;
            this.departmentService = departmentService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<string>> Handle(AddNewDepartments request, CancellationToken cancellationToken)
        {
           var dep = mapper.Map<List<Domain.Entities.Department>>(request.Departments);
           var result = await departmentService.AddDepartments(request.HR_Email , dep);
           switch (result)
            {
                case "Entered Company not Found": return BadRequest<string>(result);
                case "Departments Updated": return Success(result);
                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(AddEmployee request, CancellationToken cancellationToken)
        {
            var result = await departmentService.AddEmployeeToDepartment(request.HR_Email ,request.Department_Id ,request.Employee_email);
            if (result == "Employee added successfully to the department") return Success(result);

            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(SetManager request, CancellationToken cancellationToken)
        {
            var result = await departmentService.SetManagerForDepartment(request.HR_Email , request.DepartmentID , request.Employee_email);
            if (result == "Role updated to Manager for entered employee to company.") return Success(result);

            return BadRequest<string>(result);

        }
        #endregion
    }
}
