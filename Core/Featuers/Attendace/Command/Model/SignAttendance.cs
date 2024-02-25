using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Attendace.Command.Model
{
    public class SignAttendance : IRequest<Response<string>>
    {
        public required string Email { get; set; }
    }
}
