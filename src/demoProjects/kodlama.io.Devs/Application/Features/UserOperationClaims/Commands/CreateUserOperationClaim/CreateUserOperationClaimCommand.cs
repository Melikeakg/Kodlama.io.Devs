using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand:IRequest<CreatedUserOperationClaimDto>, ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public string[] Roles => new[] { "admin", "useroperationclaim.add" };

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserShouldExistWhenRequestes(request.UserId);

                 await _userOperationClaimBusinessRules.CanNotTakeSameClaimIfAlreadyTaken(request.UserId, request.OperationClaimId);

                UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);
                CreatedUserOperationClaimDto createdUserOperationClaimDto = _mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);
                return createdUserOperationClaimDto;

            }
        }
    }
}
