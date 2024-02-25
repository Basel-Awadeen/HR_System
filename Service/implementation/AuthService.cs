using Domain.Entities;
using Domain.Helpers;
using Infra.DBContext;
using Infra.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.IService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementation
{
    public class AuthService  : IAuthService
    {
        #region Fields
        private readonly UserManager<Employee> userManager;
        private readonly SignInManager<Employee> signInManager;
        private readonly AppDbContext dbContext;
        private readonly JwtSettings jwtSettings;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        #endregion

        #region Constructor
        public AuthService(UserManager<Employee> userManager , SignInManager<Employee> signInManager
                                                             , JwtSettings jwtSettings
                                                             , IRefreshTokenRepository refreshTokenRepository
                                                             , AppDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings;
            this.dbContext = dbContext;
            this.refreshTokenRepository = refreshTokenRepository;
        }
        #endregion

        #region other mehtods
        public async Task<string> Verfiy_OTP(string email, string code)
        {
            try
            {

                var user = await userManager.FindByNameAsync(email);
                if (user == null) return "Failed";

                else if (user != null)
                {
                    if (user.OtpCode == code && user.OtpExpirationTime > DateTime.UtcNow)
                    {
                        user.EmailConfirmed = true;
                        var updateResult = await userManager.UpdateAsync(user);
                        if (updateResult.Succeeded)
                        {
                            return "Verified";
                        }
                        return "Failed";
                    }

                    else if (user.OtpCode == code && user.OtpExpirationTime <= DateTime.UtcNow) return "Expird";

                    else return "Failed";
                }
                else return "Failed";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        #endregion

        #region Token
        public async Task<JwtAuthResult?> Login(string email, string password)
        {
            try
            {
                var currentUser = await userManager.FindByEmailAsync(email);
                if (currentUser == null)
                {
                    return null;
                }
                else if (currentUser != null)
                {
                    var signInResult = await signInManager.CheckPasswordSignInAsync(currentUser, password, false);
                    if (signInResult.Succeeded)
                    {
                        var token = await GetJWTToken(currentUser);
                        var jwtAuthResult = new JwtAuthResult
                        {
                            AccessToken = token.AccessToken,
                            refreshToken = token.refreshToken
                        };


                        return token;
                    }
                    return null;
                }
                else return null;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<JwtAuthResult> GetJWTToken(Employee user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.Email);

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                RefreshToken = refreshToken.TokenString,
                UserId = user.Id
            };

            await refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResult
            {
                AccessToken = accessToken,
                refreshToken = refreshToken
            };
            return response;
        }

        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(Employee user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddHours(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);

        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                Email = username,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        private async Task<List<Claim>> GetClaims(Employee user)
        {
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.Id),user.Id.ToString()),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber)

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            return claims;

        }

        public async Task<JwtAuthResult> GetRefreshToken(string userId, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);

            var refreshTokenResult = new RefreshToken
            {
                Email = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Email)).Value,
                TokenString = refreshToken,
                ExpireAt = (DateTime)expiryDate
            };

            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            response.refreshToken = refreshTokenResult;
            return response;

        }




        #endregion
    }
}
