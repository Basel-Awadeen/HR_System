using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vacation
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public VacationRequestStatus Status { get; set; } =VacationRequestStatus.Pending;
        public string Reason { get; set; }
        public int Day_Num { get; set; }
        public DateTime Reqeust_Date { get; set; }
        public int CompanyId { get; set; }
    }
    public enum VacationRequestStatus
    {
        Pending,
        Approved,
        Rejected
    }


}
