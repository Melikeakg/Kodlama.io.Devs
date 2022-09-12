using Application.Features.UserSocialMedias.Dtos;
using Application.Features.UserSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserSocialMedias.Command.DeleteSocialMedia
{
    public class DeleteSocialMediaCommand:IRequest<DeletedSocialMediaDto>
    {
        public int Id { get; set; }

        public class DeleteSocialMediaCommandHandler : IRequestHandler<DeleteSocialMediaCommand, DeletedSocialMediaDto>
        {
            private readonly IUserSocialMediaRepository _userSocialMediaRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaBusinessRules _userSocialMediaBusinessRules;

            public DeleteSocialMediaCommandHandler(IUserSocialMediaRepository userSocialMediaRepository, IMapper mapper, UserSocialMediaBusinessRules userSocialMediaBusinessRules)
            {
                _userSocialMediaRepository = userSocialMediaRepository;
                _mapper = mapper;
                _userSocialMediaBusinessRules = userSocialMediaBusinessRules;
            }

            public async Task<DeletedSocialMediaDto> Handle(DeleteSocialMediaCommand request, CancellationToken cancellationToken)
            {
                UserSocialMedia userSocialMedia= await _userSocialMediaRepository.GetAsync(u => u.Id == request.Id);

                _userSocialMediaBusinessRules.UserSocialMediaLinkShouldExistWhenRequested(userSocialMedia);

                UserSocialMedia deletedUserSocialMedia = await _userSocialMediaRepository.DeleteAsync(userSocialMedia);

                DeletedSocialMediaDto mappedSocialMediaCommandDto= _mapper.Map<DeletedSocialMediaDto>(deletedUserSocialMedia);

                return mappedSocialMediaCommandDto;
            }
        }

    }
}
