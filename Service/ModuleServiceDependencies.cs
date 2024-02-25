using Microsoft.Extensions.DependencyInjection;
using Service.implementation;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IVacationService, VacationService>();
            services.AddTransient<IAttendanceService, AttendanceService>();
            return services;
        }
    }
}
