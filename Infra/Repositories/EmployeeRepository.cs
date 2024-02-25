using Domain.Entities;
using Infra.Bases;
using Infra.DBContext;
using Infra.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class EmployeeRepository : GenericRepositoryAsync<Employee>, IEmployeeRepository
    {
        #region Fields
        private readonly DbSet<Employee> users;
        private readonly UserManager<Employee> userManager;
        #endregion


        #region Constructors
        public EmployeeRepository(AppDbContext dBContext, UserManager<Employee> userManager) : base(dBContext)
        {
            users = dBContext.Set<Employee>();
            this.userManager = userManager;
        }

        #endregion

        #region
        public async Task SaveOtpAsync(string email, string OTP_Code)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                user.OtpCode = OTP_Code;
                user.OtpExpirationTime = DateTime.UtcNow.AddMinutes(10);
                await userManager.UpdateAsync(user);
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            return await users
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }


        #endregion
    }
}
