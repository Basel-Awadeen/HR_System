using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAuthService
    {
        public Task<JwtAuthResult> Login(string email, string password);
        public Task<string> Verfiy_OTP(string email, string code);
        public Task<JwtAuthResult> GetJWTToken(Employee user);
        public Task<JwtAuthResult> GetRefreshToken(string userId, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);


    }
}
