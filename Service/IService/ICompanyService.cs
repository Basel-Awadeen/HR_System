using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICompanyService
    {
        public Task<string> CreateCompany (Company company);
        public Task<string> DeleteCompany (int companyId);
        public Task<string> Set_HR_Company(string Hr_Email, int CompanyId);
        public Task<List<Employee>> GetEmployeesInCompany(int companyId);
        public Task<string> SetCompanyName(string Hr_Email , string Name, string? Description);


    }
}
