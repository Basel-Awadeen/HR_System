using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IDepartmentService
    {
        public Task<string> AddDepartments(string HR_email, List<Department> departments);
        public Task<string> AddEmployeeToDepartment(string HR_email ,int departmentId, string Employee_email);
        public Task<string> SetManagerForDepartment(string HR_email, int departmentId, string Employee_email);
        public Task<(bool, int)> IsHRInCompany(string HR_email);
        public Task<string> DeleteDepartment(string HR_email , int depId);
        public Task<string> SetSalaryEmployee(string HR_email, string Emp_email, double value);




    }
}
