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
    public class VacationService : IVacationService
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
        public VacationService(ICompanyRepository companyRepository, UserManager<Employee> userManager
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
        public async Task<string> UpadeVacationRequests(string approverEmail, int vacationId, VacationRequestStatus status)
        {
            try
            {
                (bool isHRInCompany, int companyId) = await departmentService.IsHRInCompany(approverEmail);

                if (!isHRInCompany || companyId == 0) return "Not Your Company or HR";

                var vacation = await vacationRepository.GetByIdAsync(vacationId);
                if (vacation == null) return "Failed";

                if (vacation.Status ==VacationRequestStatus.Rejected) return "Request for this vacation is alaready rejected";

                vacation.Status = status;

                var employee = await userManager.FindByIdAsync(vacation.EmployeeId.ToString());
                if (status == VacationRequestStatus.Approved)
                {
                    employee.Vacation_Num = employee.Vacation_Num - vacation.Day_Num;
                }
                await vacationRepository.UpdateAsync(vacation);
                await userManager.UpdateAsync(employee);
                return "Vacation Request is Updated";
                
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        public async Task<List<Vacation>> GetVacations(string email)
        {
            try
            {
                var employee = await userManager.FindByEmailAsync(email);
                var vacatoin = await vacationRepository.GetAllVacationsAsync(employee.Id);
                return vacatoin;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving vacations", ex);
            }
        }
        #endregion
    }
}
