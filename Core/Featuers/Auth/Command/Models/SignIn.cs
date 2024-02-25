using Core.Bases;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Auth.Command.Models
{
    public class SignIn : IRequest<Response<JwtAuthResult>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
