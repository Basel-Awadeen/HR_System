using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Company.Command.Queries
{
    public class DeleteCompany : IRequest<Response<string>>
    {
        public int CompanyId { get; set; }
    }
}
