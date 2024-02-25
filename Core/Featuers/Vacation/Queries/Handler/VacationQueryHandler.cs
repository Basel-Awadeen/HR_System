using AutoMapper;
using Core.Bases;
using Core.Featuers.Vacation.Command.Models;
using Core.Featuers.Vacation.Queries.Model;
using Core.Featuers.Vacation.Queries.Result;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Vacation.Queries.Handler
{
    public class VacationQueryHandler : ResponseHandler,
        IRequestHandler<GetAllVacations, Response<List<AllVacations>>>
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IVacationService vacationService;
        #endregion

        #region Constructor
        public VacationQueryHandler(IMapper mapper, IVacationService vacationService)
        {
            this.mapper = mapper;
            this.vacationService = vacationService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<List<AllVacations>>> Handle(GetAllVacations request, CancellationToken cancellationToken)
        {
            var vacations = await vacationService.GetVacations(request.Email);
            var list = mapper.Map<List<AllVacations>>(vacations);
            var result = Success(list);
            result.Meta = new { Count = list.Count() };
            return result;
        }
        #endregion
    }
}
