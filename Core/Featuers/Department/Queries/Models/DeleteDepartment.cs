using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Department.Queries.Models
{
    public class DeleteDepartment : IRequest<Response<string>>
    {
        public string HR_Email { get; set; }
        public int DepartmentId { get; set; }
    }
}
