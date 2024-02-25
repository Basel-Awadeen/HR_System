using Core.Bases;
using Core.Featuers.Company.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Company.Queries.Models
{
    public class View_Employee : IRequest<Response<List<All_Employee_Response>>>
    {
        public required string HR_Email { get; set; }
    }
}
