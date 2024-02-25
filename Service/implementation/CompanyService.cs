using Domain.Entities;
using Infra.DBContext;
using Infra.IRepositories;
using Infra.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service.implementation
{
    public class CompanyService : ICompanyService
    {
        #region Fields
        private readonly ICompanyRepository companyRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly UserManager<Employee> userManager;
        private readonly IDepartmentService departmentService;
        private readonly AppDbContext appDbContext;
        #endregion

        #region Constructors
        public CompanyService(ICompanyRepository companyRepository , UserManager<Employee> userManager
                                                                   , IDepartmentService departmentService
                                                                   , AppDbContext appDbContext
                                                                   , IDepartmentRepository departmentRepository
                                                                   , IEmployeeRepository employeeRepository)
        {
            this.companyRepository = companyRepository;
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.appDbContext = appDbContext;
            this.departmentRepository = departmentRepository;
            this.employeeRepository = employeeRepository;
        }

        #endregion


        #region Implementation
        public async Task<string> CreateCompany(Company company)
        {
            try
            {

                await companyRepository.AddAsync(company);
                return "Company created successfully.";
            }
            catch (Exception)
            {
                return "Creating company is failed";
            }
        }

        public async Task<string> Set_HR_Company(string Hr_Email, int CompanyId)
        {
            var hr = await userManager.FindByEmailAsync(Hr_Email);
            if (hr == null) return "Employee not found";

            var company = await companyRepository.GetByIdAsync(CompanyId);
            if (company == null) return "Cannot found Enterd Company";

            var existingRoles = await userManager.GetRolesAsync(hr);
            foreach (var role in existingRoles)
            {
                await userManager.RemoveFromRoleAsync(hr, role);
            }

            await userManager.AddToRoleAsync(hr, "HR");
            hr.CompanyId = company.Id;
            await userManager.UpdateAsync(hr);

            return "Role updated to HR and assigned to company.";

        }
        public async Task<List<Employee>> GetEmployeesInCompany(int companyId)
        {
            // Retrieve all employees in the specified company including their department information
            var employees = await appDbContext.Users
                .Where(e => e.CompanyId == companyId)
                .Include(e => e.Departments)
                .ToListAsync();

            // Project the result into the desired pattern: (email, department)
            return employees;

        }

        public async Task<string> DeleteCompany(int companyId)
        {
            var existingCompany = await companyRepository.GetByIdAsync(companyId);
            if (existingCompany == null)
            {
                return "Company not found";
            }

            try
            {
                var departments = await departmentRepository.GetDepartmentsByCompanyIdAsync(companyId);
                var usersInCompany = await userManager.Users.Where(u => u.CompanyId == companyId).ToListAsync();

                if (!departments.Any())
                {
                    if (usersInCompany.Any())
                    {
                        // Delete users associated with the company
                        foreach (var user in usersInCompany)
                        {
                            await userManager.DeleteAsync(user);
                        }
                    }
  
                    await companyRepository.DeleteAsync(existingCompany);
                    await companyRepository.SaveChangesAsync();
                    return "Company deleted successfully";

                }
                foreach (var department in departments)
                {
                    await departmentRepository.DeleteAsync(department);
                }
                await departmentRepository.SaveChangesAsync();

                if (usersInCompany.Any())
                {
                    // Delete users associated with the company
                    foreach (var user in usersInCompany)
                    {
                        await userManager.DeleteAsync(user);
                    }
                }

                await companyRepository.DeleteAsync(existingCompany);
                await companyRepository.SaveChangesAsync();


                return "Company and associated departments deleted successfully";
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                return $"Error deleting company: {ex.Message}";
            }

        }

        public async Task<string> SetCompanyName(string Hr_Email ,string Name ,string? Description)
        {
            (bool isHRInCompany, int companyId) = await departmentService.IsHRInCompany(Hr_Email);

            if (!isHRInCompany || companyId == 0) return "Entered Company not Found";

            var copmany = await companyRepository.GetByIdAsync(companyId);
            if (copmany == null) return "Entered Company not Found";
            if (string.IsNullOrEmpty(Description))
            {
                copmany.Name = Name;
                copmany.Description = copmany.Description;
                await companyRepository.UpdateAsync(copmany);
                return "Company Details is Updated";
            }
            copmany.Name = Name;
            copmany.Description = Description;

            await companyRepository.UpdateAsync(copmany);
            return "Company Details is Updated";
        }

        #endregion

    }
}
