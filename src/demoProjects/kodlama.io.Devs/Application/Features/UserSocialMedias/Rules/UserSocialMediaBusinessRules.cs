using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.UserSocialMedias.Rules
{
    public class UserSocialMediaBusinessRules
    {
        private readonly IUserSocialMediaRepository _userSocialMediaRepository;

        public UserSocialMediaBusinessRules(IUserSocialMediaRepository userSocialMediaRepository)
        {
            _userSocialMediaRepository = userSocialMediaRepository;
        }

        public async Task CheckForSocialMediaLinkExists(string socialMediaLink)
        {
            var result = await _userSocialMediaRepository.GetListAsync(u => u.SocialMediaLink == socialMediaLink);
            if (result.Items.Any())
            {
                throw new BusinessException("Social media link already exists!");
            }
        }

        public void UserSocialMediaLinkShouldExistWhenRequested(UserSocialMedia userSocialMedia)
        {
            if (userSocialMedia==null)
            {
                throw new BusinessException("Social media link does not exists");
            }
        }
    }
}
