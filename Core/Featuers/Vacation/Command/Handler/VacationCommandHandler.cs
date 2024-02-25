using AutoMapper;
using Core.Bases;
using Core.Featuers.Employee.Command.Model;
using Core.Featuers.Vacation.Command.Models;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Vacation.Command.Handler
{
    public class VacationCommandHandler : ResponseHandler,
        IRequestHandler<UpdateVacation, Response<string>>
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IVacationService vacationService;
        #endregion

        #region Constructor
        public VacationCommandHandler(IMapper mapper, IVacationService vacationService)
        {
            this.mapper = mapper;
            this.vacationService = vacationService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<string>> Handle(UpdateVacation request, CancellationToken cancellationToken)
        {
            var result = await vacationService.UpadeVacationRequests(request.ApproverEmail, request.VacationId, request.Status);
            if (result == "Vacation Request is Updated") return Success(result);
            return BadRequest<string>(result);
        }
        #endregion
    }
}
