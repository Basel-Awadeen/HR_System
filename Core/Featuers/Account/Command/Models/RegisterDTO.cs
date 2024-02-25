using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Account.Command.Models
{
    public class RegisterDTO : IRequest<Response<string>>
    {
        public required string FName { get; set; }
        public required string LName { get; set; }
        public required string Address { get; set; }
        public required string SSN { get; set; }
        [EmailAddress (ErrorMessage ="Invalid Emaill address")]
        public required string Email { get; set; }

        [Phone (ErrorMessage ="invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Invalid phone number format")]
        public required string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }

    }
}
