using Domain.Entities;
using Infra.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IRepositories
{
    public interface IEmployeeRepository : IGenericRepositoryAsync<Employee>
    {
        public Task SaveOtpAsync(string email, string OTP_Code);
        public Task<List<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId);

    }
}
