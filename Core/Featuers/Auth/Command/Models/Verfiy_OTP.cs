using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Auth.Command.Models
{
    public class Verfiy_OTP : IRequest<Response<string>>
    {
        public required string email { get; set; }
        public required string OTP_Code { get; set; }
    }
}
