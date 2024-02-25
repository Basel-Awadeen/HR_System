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
    public class AttendanceService : IAttendanceService
    {
        #region Fields
        private readonly ICompanyRepository companyRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly UserManager<Employee> userManager;
        private readonly IDepartmentService departmentService;
        private readonly AppDbContext appDbContext;
        private readonly IAttendanceRepository attendanceRepository;
        #endregion

        #region Constructors
        public AttendanceService(ICompanyRepository companyRepository, UserManager<Employee> userManager
                                                                   , IDepartmentService departmentService
                                                                   , AppDbContext appDbContext
                                                                   , IDepartmentRepository departmentRepository
                                                                   , IEmployeeRepository employeeRepository, IAttendanceRepository attendanceRepository)
        {
            this.companyRepository = companyRepository;
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.appDbContext = appDbContext;
            this.departmentRepository = departmentRepository;
            this.employeeRepository = employeeRepository;
            this.attendanceRepository = attendanceRepository;
        }


        #endregion

        #region Implementaiton
        public async Task<string> Attend(string email)
        {
            try
            {
                var employee = await userManager.FindByEmailAsync(email);
                if (employee == null) return "Failed";
                var attend = new Attendance()
                {
                    EmployeeId = employee.Id,
                    DepartmentId = (int)employee.DepartmentId,
                    CompanyId = (int)employee.CompanyId,
                    Employee_Email = email,
                    AttendanceDateTime = DateTime.Now,
                    Status = CheckAttendance(DateTime.Now),
                };

                await attendanceRepository.AddAsync(attend);
                return "Attended Success";
            }
            catch (Exception)
            {
                return "Failed";
            }

        }

        public async Task<List<Attendance>> GetAttendance(string email)
        {
            try
            {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return new List<Attendance>();
            var attendance = await attendanceRepository.GetAttendanceListAsync(user.Id);
                return attendance;
            }
            catch
            {
                return new List<Attendance>();
            }

        }
        #endregion

        #region methods
        public AttendanceStatus CheckAttendance(DateTime attendanceTime)
        {
            TimeSpan lateThreshold = new TimeSpan(8, 0, 0); // 8:00 AM
            TimeSpan arrivalTime = attendanceTime.TimeOfDay;

            if (arrivalTime > lateThreshold)
            {
                return AttendanceStatus.Late;
            }
            else
            {
                return AttendanceStatus.Present;
            }
        }

        #endregion

    }
}
