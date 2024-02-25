using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Employee : IdentityUser<int>
    {
        public Employee() 
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();

        }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public string? SSN { get; set; }
        [EncryptColumn]
        public string? OtpCode { get; set; }
        public DateTime? OtpExpirationTime { get; set; }

        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
        public int? DepartmentId { get; set; } 
        public Department Departments { get; set; }
        public int? CompanyId { get; set; } 
        public virtual Company Company { get; set; }
        public int Vacation_Num { get; set; } = 21;
        public double BasicSalary { get; set; } = 0.0;


    }
}
