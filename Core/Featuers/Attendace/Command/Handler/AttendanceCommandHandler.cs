using AutoMapper;
using Core.Bases;
using Core.Featuers.Attendace.Command.Model;
using Core.Featuers.Attendace.Queries.Models;
using Core.Featuers.Attendace.Queries.Results;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Attendace.Command.Handler
{
    public class AttendanceCommandHandler : ResponseHandler,
        IRequestHandler<SignAttendance, Response<string>>
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IAttendanceService attendanceService;
        #endregion

        #region Constructor
        public AttendanceCommandHandler(IMapper mapper, IAttendanceService attendanceService)
        {
            this.mapper = mapper;
            this.attendanceService = attendanceService;
        }

        #endregion

        #region Handel Requests
        public async Task<Response<string>> Handle(SignAttendance request, CancellationToken cancellationToken)
        {
            var result = await attendanceService.Attend(request.Email);
            if(result == "Attended Success") return Success(result);
            return BadRequest<string>();
        }
        #endregion
    }
}
