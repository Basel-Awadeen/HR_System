using Domain.Entities;
using Infra.DBContext;
using Infra.IRepositories;
using Microsoft.AspNetCore.Identity;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementation
{
    public class EmployeeService: IEmployeeService
    {
        #region Fields
        private readonly ICompanyRepository companyRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly UserManager<Employee> userManager;
        private readonly IDepartmentService departmentService;
        private readonly AppDbContext appDbContext;
        private readonly IVacationRepository vacationRepository;
        #endregion

        #region Constructors
        public EmployeeService(ICompanyRepository companyRepository, UserManager<Employee> userManager
                                                                   , IDepartmentService departmentService
                                                                   , AppDbContext appDbContext
                                                                   , IDepartmentRepository departmentRepository
                                                                   , IEmployeeRepository employeeRepository, IVacationRepository vacationRepository)
        {
            this.companyRepository = companyRepository;
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.appDbContext = appDbContext;
            this.departmentRepository = departmentRepository;
            this.employeeRepository = employeeRepository;
            this.vacationRepository = vacationRepository;
        }


        #endregion

        #region Implementation
        public async Task<string> RequestVacation(string Emp_Email, string reason, int num)
        {
            try
            {
            var employee = await userManager.FindByEmailAsync(Emp_Email);
            if (employee == null) return "Cannot Found Employee";

            var vacation = new Vacation()
            {
                EmployeeId = employee.Id,
                EmployeeEmail = employee.Email,
                Status = VacationRequestStatus.Pending,
                Reason = reason,
                Day_Num = num,
                CompanyId = (int)employee.CompanyId,
                Reqeust_Date = DateTime.Now
            };
            await vacationRepository.AddAsync(vacation);

            await userManager.UpdateAsync(employee);
            return "Vaction Requested is Successfully";
            }
            catch(Exception)
            {
                return "Request Vacation is Failed";
            }

        }
       
        
        #endregion
    }
}