using Application.Features.UserSocialMedias.Dtos;
using Application.Features.UserSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMedias.Command.CreateSocialMedia
{
    public class CreateSocialMediaCommand:IRequest<CreatedSocialMediaDto>
    {
        public int UserId { get; set; }
        public string SocialMediaLink { get; set; }

        public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand, CreatedSocialMediaDto>
        {
            private readonly IUserSocialMediaRepository _userSocialMediaRespository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaBusinessRules _userSocialMediaBusinessRules;

            public CreateSocialMediaCommandHandler(IUserSocialMediaRepository userSocialMediaRespository, IMapper mapper, UserSocialMediaBusinessRules userSocialMediaBusinessRules)
            {
                _userSocialMediaRespository = userSocialMediaRespository;
                _mapper = mapper;
                _userSocialMediaBusinessRules = userSocialMediaBusinessRules;
            }

            public async Task<CreatedSocialMediaDto> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
            {
                UserSocialMedia mappedUserSocialMedia=  _mapper.Map<UserSocialMedia>(request);

                UserSocialMedia createdUserSocialMedia=await _userSocialMediaRespository.AddAsync(mappedUserSocialMedia);

                CreatedSocialMediaDto createdSocialMediaCommandDto= _mapper.Map<CreatedSocialMediaDto>(createdUserSocialMedia);

                return createdSocialMediaCommandDto;
            }
        }
    }
}
