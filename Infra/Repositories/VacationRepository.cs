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
    public class VacationRepository : GenericRepositoryAsync<Vacation>, IVacationRepository
    {
        #region Fields
        private readonly DbSet<Vacation> vacations;
        #endregion

        #region Constructors
        public VacationRepository(AppDbContext dbContext) : base(dbContext)
        {
            vacations = dbContext.Set<Vacation>();
        }


        #endregion

        #region Handle Func
        public async Task<List<Vacation>> GetAllVacationsAsync(int employee_ID)
        {
            return await vacations.Where(e => e.EmployeeId == employee_ID).ToListAsync(); 
        }
        #endregion
    }
}
