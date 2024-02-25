using Domain.Entities;
using Infra.DBContext;
using Infra.IRepositories;
using Infra.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.IService;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service.implementation
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository departmentRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly UserManager<Employee> userManager;
        private readonly AppDbContext appDbContext;


        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository, ICompanyRepository companyRepository
                                                                            , UserManager<Employee> userManager
                                                                            , AppDbContext appDbContext)
        {
            this.departmentRepository = departmentRepository;
            this.companyRepository = companyRepository;
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }


        #endregion


        #region Implementation
        public async Task<string> AddDepartments(string HR_email, List<Department> departments)
        {
            (bool isHRInCompany, int companyId) = await IsHRInCompany(HR_email);

            if (!isHRInCompany || companyId==0) return "Entered Company not Found";

            else if (departments != null)
            {
                var new_departments = departments.Select(x => new Department
                {
                    Name = x.Name,
                    Description = x.Description,
                    CompanyId = companyId,
                }).ToList();
                await departmentRepository.AddRangeAsync(new_departments);
                return "Departments Updated";
            }

            return "Departments added successfully";

        }

        public async Task<string> AddEmployeeToDepartment(string HR_email, int departmentId, string Employee_email)
        {
            (bool isHRInCompany, int companyId) = await IsHRInCompany(HR_email);

            if (!isHRInCompany || companyId == 0) return "Not Your Company or HR";

            bool isDepartmentInCompany = await IsDepartmentInCompany(companyId, departmentId);
            if (!isDepartmentInCompany) return "Cannot Find Department in company";

            var user = await userManager.FindByEmailAsync(Employee_email);
            user.CompanyId = companyId;
            user.DepartmentId = departmentId;

            var result = await userManager.UpdateAsync(user);
            if(result.Succeeded) return "Employee added successfully to the department";

            return "Failed";
        }

        public async Task<(bool,int)> IsHRInCompany(string HR_email)
        {
            if (HR_email == null)
                return (false,0);
            var user = await userManager.FindByEmailAsync(HR_email);
            if (user == null)
                return (false, 0);

            bool isInHRRole = await userManager.IsInRoleAsync(user, "HR");

            if (user.CompanyId is null) return (false, 0);
            var company = await companyRepository.GetByIdAsync(user.CompanyId.Value);
            if (isInHRRole && user.CompanyId == company.Id)
            {
              return (true,company.Id);
     
            }

            return (false, 0);
        }
        public async Task<bool> IsDepartmentInCompany(int companyId, int departmentId)
        {
            var department = await departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
            {
                throw new ArgumentException("Department not found");
            }

            return department.CompanyId == companyId;
        }

        public async Task<string> DeleteDepartment(string HR_email, int depId)
        {
            (bool isHRInCompany, int companyId) = await IsHRInCompany(HR_email);

            if (!isHRInCompany || companyId == 0) return "Not Your Company or HR";

            var department = await departmentRepository.GetByIdAsync(depId);
            if (department == null)
            {
                return "Dont found any Departments for this company ";
            }
            
            var employeesInDepartment = await userManager.Users.Where(u => u.DepartmentId == department.Id).ToListAsync();
            if (employeesInDepartment == null)
            {
                await departmentRepository.DeleteAsync(department);
                await departmentRepository.SaveChangesAsync();

                return "Department Deleted successfully";
            }

            foreach (var employee in employeesInDepartment)
            {
                employee.DepartmentId = null;
            }
            
            await departmentRepository.DeleteAsync(department);
            await departmentRepository.SaveChangesAsync();

            await appDbContext.SaveChangesAsync();
            return "Departments and user in depatmend is deleted successfully";
        }

        public async Task<string> SetManagerForDepartment(string HR_email, int departmentId, string Employee_email)
        {
            (bool isHRInCompany, int companyId) = await IsHRInCompany(HR_email);

            if (!isHRInCompany || companyId == 0) return "Not Your Company or HR";

            bool isDepartmentInCompany = await IsDepartmentInCompany(companyId, departmentId);
            if (!isDepartmentInCompany) return "Cannot Find Department in company";

            var user = await userManager.FindByEmailAsync(Employee_email);
            if (user == null) return "Cannot Found Employee";

            var existingRoles = await userManager.GetRolesAsync(user);
            foreach (var role in existingRoles)
            {
                await userManager.RemoveFromRoleAsync(user, role);
            }

            await userManager.AddToRoleAsync(user, "manager");
            await userManager.UpdateAsync(user);

            return "Role updated to Manager for entered employee to company.";

        }

        public Task<string> SetSalaryEmployee(string HR_email, double value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
