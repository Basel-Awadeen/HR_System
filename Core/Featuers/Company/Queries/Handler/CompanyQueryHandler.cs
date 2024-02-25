using AutoMapper;
using Core.Bases;
using Core.Featuers.Company.Command.Queries;
using Core.Featuers.Company.Queries.Models;
using Core.Featuers.Company.Queries.Results;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Company.Queries.Handler
{
    public class CompanyQueryHandler : ResponseHandler,
        IRequestHandler<View_Employee, Response<List<All_Employee_Response>>>
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly ICompanyService companyService;
        private readonly IDepartmentService departmentService;
        #endregion

        #region Constructor
        public CompanyQueryHandler(IMapper mapper, ICompanyService companyService , IDepartmentService departmentService)
        {
            this.mapper = mapper;
            this.companyService = companyService;
            this.departmentService = departmentService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<List<All_Employee_Response>>> Handle(View_Employee request, CancellationToken cancellationToken)
        {
            (bool isHRInCompany, int companyId) = await departmentService.IsHRInCompany(request.HR_Email);
            if (isHRInCompany)
            {
                var employees = await companyService.GetEmployeesInCompany(companyId);
                var emp_list = mapper.Map<List<All_Employee_Response>>(employees);
                var result = Success(emp_list);
                result.Meta = new { Count = emp_list.Count() };
                return result;
            }
            return BadRequest<List<All_Employee_Response>>("");
        }
        #endregion
    }
}
