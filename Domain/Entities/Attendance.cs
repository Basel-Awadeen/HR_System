using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public DateTime AttendanceDateTime { get; set; }
        public AttendanceStatus Status { get; set; }
        public int EmployeeId { get; set; }
        public string Employee_Email { get; set; }
        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }

    }
    public enum AttendanceStatus
    {
        Present,
        Late,
    }
}
