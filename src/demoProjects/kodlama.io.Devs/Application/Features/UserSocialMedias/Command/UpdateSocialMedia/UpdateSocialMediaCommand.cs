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

namespace Application.Features.UserSocialMedias.Command.UpdateSocialMedia
{
    public class UpdateSocialMediaCommand:IRequest<UpdatedSocialMediaDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SocialMediaLink { get; set; }

        public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand, UpdatedSocialMediaDto>
        {
            private readonly IUserSocialMediaRepository _userSocialMediaRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaBusinessRules _userSocialMediaBusinessRules;

            public UpdateSocialMediaCommandHandler(IUserSocialMediaRepository userSocialMediaRepository, IMapper mapper, UserSocialMediaBusinessRules userSocialMediaBusinessRules)
            {
                _userSocialMediaRepository = userSocialMediaRepository;
                _mapper = mapper;
                _userSocialMediaBusinessRules = userSocialMediaBusinessRules;
            }

            public async Task<UpdatedSocialMediaDto> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
            {
                UserSocialMedia userSocialMedia = await _userSocialMediaRepository.GetAsync(u => u.Id == request.Id);

                _userSocialMediaBusinessRules.UserSocialMediaLinkShouldExistWhenRequested(userSocialMedia);

                userSocialMedia.UserId = request.UserId;
                userSocialMedia.SocialMediaLink = request.SocialMediaLink;

                _userSocialMediaBusinessRules.CheckForSocialMediaLinkExists(userSocialMedia.SocialMediaLink);

                UserSocialMedia updatedSocialMedia=await _userSocialMediaRepository.UpdateAsync(userSocialMedia);
                UpdatedSocialMediaDto mappedSocialMediaDto= _mapper.Map<UpdatedSocialMediaDto>(updatedSocialMedia);

                return mappedSocialMediaDto;

            }
        }
    }
}
