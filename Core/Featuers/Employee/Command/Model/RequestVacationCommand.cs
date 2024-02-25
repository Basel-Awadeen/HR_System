using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Employee.Command.Model
{
    public class RequestVacationCommand : IRequest<Response<string>>
    {
        public required string Employee_Email { get; set; }
        public required string Reason { get; set; }
        public int Day_Number { get; set; }
    }
}
