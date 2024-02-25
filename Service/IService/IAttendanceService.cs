using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAttendanceService
    {
        public Task<string> Attend(string email);
        public Task<List<Attendance>> GetAttendance(string email);
    }
}
