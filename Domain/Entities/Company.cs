using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Vacation>? VacationRequests { get; set; } =new List<Vacation>();


    }
}
