using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Department.Command.Models
{
    public class New_Salary : IRequest<Response<string>>
    {
        public string HR_Email { get; set; }
        public string Employee_Email { get; set; }//hjgh
        public double Salary { get; set; }
    }
}
