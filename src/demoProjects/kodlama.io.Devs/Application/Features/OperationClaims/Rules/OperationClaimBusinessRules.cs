using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimNameCanNotBeDuplicatedWhenInserted(string name)
        {
            OperationClaim? result = await _operationClaimRepository.GetAsync(o => o.Name == name);
            if (result!=null)
            {
                throw new BusinessException("Operation claim name already exists.");
            }
        }

        public void OperationClaimShouldExistWhenRequested(OperationClaim operationClaim)
        {
            if (operationClaim==null)
            {
                throw new BusinessException("Requested operation claim does not exists");
            }
            
        }
    }
}
