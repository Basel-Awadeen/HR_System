using AutoMapper;
using Core.Bases;
using Core.Featuers.Account.Command.Models;
using Domain.Entities;
using MediatR;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Account.Command.Handler
{
    public class AccountCommandHandler : ResponseHandler,
                                  IRequestHandler<RegisterDTO, Response<string>>
    {
        #region Fildes
        private readonly IAccountService IaccountService;
        private readonly IMapper mapper;

        #endregion

        #region Constructors
        public AccountCommandHandler(IAccountService accountService, IMapper mapper)
        {
            this.IaccountService = accountService;
            this.mapper = mapper;
        }

        #endregion

        #region HandelFunctions
        public async Task<Response<string>> Handle(RegisterDTO request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<Domain.Entities.Employee>(request);
            var result = await IaccountService.Register(user,request.Password);
            switch (result)
            {
                case "Email is Exist": return BadRequest<string>(result.ToString());
                case "PhoneNumber is Exist": return BadRequest<string>(result.ToString());
                case "Failed To Create Account": return BadRequest<string>(result.ToString());
                case "Account is Created": return Success<string>(result);
                default: return BadRequest<string>(result);
            }
        }
        #endregion
    }
}
