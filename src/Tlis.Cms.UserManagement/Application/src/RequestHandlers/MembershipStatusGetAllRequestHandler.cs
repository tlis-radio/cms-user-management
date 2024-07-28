using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class MembershipStatusGetAllRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MembershipStatusGetAllRequest, MembershipStatusGetAllResponse>
{
    public async Task<MembershipStatusGetAllResponse> Handle(MembershipStatusGetAllRequest request, CancellationToken cancellationToken)
    {
        var memberships = await unitOfWork.MembershipRepository.GetAll();

        return new MembershipStatusGetAllResponse
        {
            Results = memberships.Select(membership => new MembershipStatusGetAllResponseItem
            {
                Id = membership.Id,
                Status = Enum.GetName(membership.Status) ?? throw new Exception($"Unable to Enum.GetName for {membership.Status}")
            }).ToList()
        };
    }
}