using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task UserShouldExistWhenRequestes(int userId)
        {
            UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(u => u.UserId == userId);
            if (result ==null)
            {
                throw new BusinessException("User does not exists.");
            }
        }

        public async Task CanNotTakeSameClaimIfAlreadyTaken(int userId, int operatinClaimId)
        {
            IPaginate<UserOperationClaim> result =await _userOperationClaimRepository.GetListAsync(u => u.UserId == userId);
            if (result.Items.Any(o=>o.OperationClaimId==operatinClaimId))
            {
                throw new BusinessException("The user already taken that claim.");
            }
        }

        public async Task UserOperationClaimShouldExistWhenRequested(int id)
        {
            UserOperationClaim result = await _userOperationClaimRepository.GetAsync(x => x.Id == id);
            if (result==null)
            {
                throw new BusinessException("User operation claim does not exists.");
            }
        }
  

    }
}
