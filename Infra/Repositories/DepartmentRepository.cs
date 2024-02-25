using Domain.Entities;
using Infra.Bases;
using Infra.DBContext;
using Infra.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        #region Fields
        private DbSet<Department> departments;
        #endregion

        #region Constructors
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
            departments = dbContext.Set<Department>();
        }
        #endregion

        #region
        public async Task<List<Department>> GetDepartmentsByCompanyIdAsync(int companyId)
        {
            return await departments
                .Where(d => d.CompanyId == companyId)
                .ToListAsync();
        }
        #endregion

    }
}
