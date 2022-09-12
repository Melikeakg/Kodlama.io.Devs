using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using MediatR;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand:IRequest<UserLoginDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class UserRegisterCommandHandler : IRequestHandler<RegisterUserCommand, UserLoginDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public UserRegisterCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UserLoginDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                _userBusinessRules.EmailCanNotBeDuplicated(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                User user = _mapper.Map<User>(request);
                user.Status = true;
                user.PasswordSalt = passwordSalt;
                user.PasswordHash=passwordHash;
                user.AuthenticatorType= AuthenticatorType.Email;

                User registeredUser = await _userRepository.AddAsync(user);
                UserLoginDto userDto= _mapper.Map<UserLoginDto>(registeredUser);
                return userDto;
            }
        }
    }
}
