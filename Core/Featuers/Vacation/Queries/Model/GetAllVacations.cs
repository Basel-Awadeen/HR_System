using Core.Bases;
using Core.Featuers.Company.Queries.Results;
using Core.Featuers.Vacation.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Vacation.Queries.Model
{
    public class GetAllVacations : IRequest<Response<List<AllVacations>>>
    {
        public string Email { get; set; }

    }
}
