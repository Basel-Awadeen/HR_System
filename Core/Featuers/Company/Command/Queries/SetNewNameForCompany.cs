using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Company.Command.Queries
{
    public class SetNewNameForCompany : IRequest<Response<string>>
    {
        public required string HR_Email { get; set; }
        public required string  Name{ get; set; }
        public string? Description { get; set; }
    }
}
