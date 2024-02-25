using AutoMapper;
using Core.Bases;
using Core.Featuers.Company.Command.Queries;
using Domain.Entities;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Company.Command.Handler
{
    public class CompanyCommandHandler : ResponseHandler,
        IRequestHandler<CreateCompanyCommand, Response<string>>
      , IRequestHandler<NewHr, Response<string>>
      , IRequestHandler<DeleteCompany, Response<string>>
      , IRequestHandler<SetNewNameForCompany, Response<string>>


    {
        #region Fields
        private readonly IMapper mapper;
        private readonly ICompanyService companyService;
        #endregion

        #region Constructor
        public CompanyCommandHandler(IMapper mapper , ICompanyService companyService)
        {
            this.mapper = mapper;
            this.companyService = companyService;
        }


        #endregion

        #region Handel Requests
        public async Task<Response<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {

                var company = mapper.Map<Domain.Entities.Company>(request);
                var result = await companyService.CreateCompany(company);
                if (result.ToString() == "Company created successfully.") return Success<string>("Company created successfully.");
                else return BadRequest<string>("Creating company is failed");
        }

        public async Task<Response<string>> Handle(NewHr request, CancellationToken cancellationToken)
        {
            var result = await companyService.Set_HR_Company(request.HR_Email, request.CompanyID);
            if (result == "Role updated to HR and assigned to company.") return Success(result);
            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteCompany request, CancellationToken cancellationToken)
        {
            var result = await companyService.DeleteCompany(request.CompanyId);
            if (result == "Company and associated departments deleted successfully") return Success(result);
            if (result == "Company deleted successfully") return Success(result);
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(SetNewNameForCompany request, CancellationToken cancellationToken)
        {
            var result = await companyService.SetCompanyName(request.HR_Email, request.Name, request.Description);
            if (result == "Company Details is Updated") return Success(result);
            return BadRequest<string>(result);

        }
        #endregion
    }
}
