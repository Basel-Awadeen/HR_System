using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Department.Command.Models
{
    public class AddEmployee : IRequest<Response<string>>
    {
        public required string HR_Email { get; set; }
        public required int Department_Id { get; set; }
        public required string Employee_email { get; set; }
    }
}
