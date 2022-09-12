﻿using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand:IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessToken>
        {
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserCommandHandler(ITokenHelper tokenHelper, IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _tokenHelper = tokenHelper;
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.Email == request.Email);
                _userBusinessRules.UserShouldExistWhenRequested(user);

                _userBusinessRules.VerifyPassword(request.Password,user.PasswordHash, user.PasswordSalt);

                var operationClaims = _userRepository.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, operationClaims);
                return accessToken;
            }
        }
    }
}
