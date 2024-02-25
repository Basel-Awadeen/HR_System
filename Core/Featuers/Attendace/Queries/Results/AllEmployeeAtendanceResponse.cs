using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Attendace.Queries.Results
{
    public class AllEmployeeAtendanceResponse
    {
        public string Employee_Email { get; set; }
        public DateTime AttendanceDateTime { get; set; }
        public AttendanceStatus Status { get; set; }

    }
}
