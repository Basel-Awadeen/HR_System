using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IEmployeeService
    {
        public Task<string> RequestVacation(string Emp_Email, string reason, int num);
    }
}
