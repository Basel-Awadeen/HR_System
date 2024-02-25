using AutoMapper;
using Core.Bases;
using Core.Featuers.Company.Command.Queries;
using Core.Featuers.Department.Queries.Models;
using MailKit;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Department.Queries.Handler
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<DeleteDepartment, Response<string>>
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IDepartmentService departmentService;
        #endregion

        #region Constructor
        public DepartmentQueryHandler(IMapper mapper, IDepartmentService departmentService)
        {
            this.mapper = mapper;
            this.departmentService = departmentService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<string>> Handle(DeleteDepartment request, CancellationToken cancellationToken)
        {
            var result = await departmentService.DeleteDepartment(request.HR_Email, request.DepartmentId);
            if (result == "Department Deleted successfully") return Success(result);
            if (result == "Departments and user in depatmend is deleted successfully") return Success(result);
            return BadRequest<string>(result);
        }

        #endregion
    }
}
