using AutoMapper;
using Core.Bases;
using Core.Featuers.Attendace.Queries.Models;
using Core.Featuers.Attendace.Queries.Results;
using Core.Featuers.Company.Queries.Models;
using Core.Featuers.Company.Queries.Results;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Attendace.Queries.Handler
{
    public class AttendanceQueryHandler : ResponseHandler,
        IRequestHandler<GetAttendanceEmployee, Response<List<AllEmployeeAtendanceResponse>>>
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IAttendanceService attendanceService;
        #endregion

        #region Constructor
        public AttendanceQueryHandler(IMapper mapper, IAttendanceService attendanceService)
        {
            this.mapper = mapper;
            this.attendanceService = attendanceService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<List<AllEmployeeAtendanceResponse>>> Handle(GetAttendanceEmployee request, CancellationToken cancellationToken)
        {
            var attendance = await attendanceService.GetAttendance(request.Email);
            var list = mapper.Map<List<AllEmployeeAtendanceResponse>>(attendance);
            var result = Success(list);
            result.Meta = new { Count = list.Count };
            return result;
        }

        #endregion
    }
}
