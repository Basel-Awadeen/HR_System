using Core.Bases;
using Core.Featuers.Attendace.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Attendace.Queries.Models
{
    public class GetAttendanceEmployee : IRequest<Response<List<AllEmployeeAtendanceResponse>>>
    {
        public required string Email { get; set; }
    }
}
