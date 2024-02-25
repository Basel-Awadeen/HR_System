using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.DBContext;
using Service.IService;
using Infra.IRepositories;

namespace Service.implementation
{
    public class AccountService : IAccountService
    {
        #region Fields
        private readonly UserManager<Employee> userManager;
        private readonly AppDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmailService emailService;
        private readonly IEmployeeRepository userRepository;


        #endregion

        #region Constuctors
        public AccountService(UserManager<Employee> userManager , AppDbContext dbContext
                                                                , IEmailService emailService
                                                                , IEmployeeRepository userRepository)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.emailService = emailService;
            this.userRepository = userRepository;
        }


        #endregion

        #region Functions

        public async Task<string> Register(Employee user, string password)
        {
            var trans = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var existEmail = await userManager.FindByEmailAsync(user.Email);

                if (existEmail != null) return "Email is Exist";

                // if PhoneNumber is Exist
                var exsitPhone = await userManager.FindByNameAsync(user.PhoneNumber);
                if (exsitPhone != null) return "PhoneNumber is Exist";

                user.UserName = user.Email;
                var result = await userManager.CreateAsync(user , password);
                if (result.Succeeded)
                {
                    var verificationCode = GenerateOTP();
                    await userRepository.SaveOtpAsync(user.Email, verificationCode);


                    var message = $@"
                                       <p>Hello,</p>
                                       <p>Thank you for choosing HR! To complete your registration, please use the following verification code:</p>
                                       <p><strong>Verification Code:</strong> {verificationCode}</p>
                                       <p>Please enter this code on the registration page to verify your account. If you did not sign up for an account with HR, please disregard this email.</p>
                                       <p>Best Regards,<br/>The HR Team</p>
                                        ";
                    await emailService.SendEmailAsync(user.Email, message, "[HR System] Account Verification Code");

                    await userManager.AddToRoleAsync(user,"regular employee");
                    await trans.CommitAsync();

                    return "Account is Created";
                }
                return "Failed To Create Account";
            }
            catch(Exception)
            {
                return "Failed To Create Account";

            }

        }

        #endregion

        #region methods
        public string GenerateOTP()
        {
            var chars = "0123456789";
            var random = new Random();
            var randomNumber = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomNumber;
        }
        #endregion
    }
}
