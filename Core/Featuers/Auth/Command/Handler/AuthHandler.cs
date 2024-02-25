using AutoMapper;
using Core.Bases;
using Core.Featuers.Auth.Command.Models;
using Domain.Helpers;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Auth.Command.Handler
{
    public class AuthHandler: ResponseHandler,
                                  IRequestHandler<Verfiy_OTP, Response<string>>
                                  , IRequestHandler<SignIn, Response<JwtAuthResult>>

    {
        #region Fildes

        private readonly IAuthService authService;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public AuthHandler(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;

        }




        #endregion

        #region HandleFunctions
        public async Task<Response<string>> Handle(Verfiy_OTP request, CancellationToken cancellationToken)
        {
           var result = await authService.Verfiy_OTP(request.email, request.OTP_Code);
           if (result == "Verified") return Success(result);
           else if (result == "Expird") return BadRequest<string>(result);
           else if (result == "Failed") return BadRequest<string>(result);
           else return BadRequest<string>("Failed");

        }

        public async Task<Response<JwtAuthResult>> Handle(SignIn request, CancellationToken cancellationToken)
        {
            var result = await authService.Login(request.Email, request.Password);

            if (result != null) return Success(result);

            else return BadRequest<JwtAuthResult>("Invalid user or user not found");
        }
        #endregion
    }
}
