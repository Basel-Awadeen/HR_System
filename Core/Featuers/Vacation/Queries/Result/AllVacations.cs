using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Vacation.Queries.Result
{
    public class AllVacations
    {
        public string EmployeeEmail { get; set; }
        public DateTime Reqeust_Date { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }

    }
}
