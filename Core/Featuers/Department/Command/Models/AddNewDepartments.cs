using Core.Bases;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Department.Command.Models
{
    public class AddNewDepartments : IRequest<Response<string>>
    {
        public required string HR_Email { get; set; } // Id of the company to which departments will be added
        public required List<DepartmentDto> Departments { get; set; }

        public class DepartmentDto
        {
            public required string Name { get; set; }
            public string? Description { get; set; }
        }

    }
}
