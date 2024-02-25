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
    public class AttendanceRepository : GenericRepositoryAsync<Attendance>, IAttendanceRepository
    {
        #region Fields
        private readonly DbSet<Attendance> AttendanceRecord;
        #endregion

        #region Constructors
        public AttendanceRepository(AppDbContext dbContext) : base(dbContext)
        {
            AttendanceRecord = dbContext.Set<Attendance>();
        }
        #endregion

        #region Func
        public async Task<List<Attendance>> GetAttendanceListAsync(int empId)
        {
            return await AttendanceRecord.Where(e => e.EmployeeId == empId).ToListAsync();
        }
        #endregion
    }
}
