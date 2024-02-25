using Domain.Entities;
using Infra.DBContext;
using Infra.IRepositories;
using Infra.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<Employee> _userManager , AppDbContext dbContext)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                // Fetch an existing company or create a new one
                var company = await dbContext.Companies.FirstOrDefaultAsync();
                if (company == null)
                {
                    // If no company exists, create a new one
                    company = new Company
                    {
                        Name = "Default Company",
                        Description = "Default Company Description"
                    };
                    dbContext.Companies.Add(company);
                    await dbContext.SaveChangesAsync();

                }



                var user = new Employee()
                {

                    UserName = "admin",
                    Email = "admin@project.com",
                    PhoneNumber = "123456",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CompanyId = 1 
                };
                await _userManager.CreateAsync(user, "M123_m");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }

    }
}
