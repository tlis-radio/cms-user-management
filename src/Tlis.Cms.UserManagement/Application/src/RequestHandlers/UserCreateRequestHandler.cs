using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;
using Tlis.Cms.UserManagement.Application.Mappers;
using Tlis.Cms.UserManagement.Domain.Constants;
using System.Linq;
using System;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserCreateRequestHandler(
    IAuthProviderManagementService authProviderManagementService,
    IRoleService roleService,
    IUnitOfWork unitOfWork) : IRequestHandler<UserCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
    {
        var userToCreate = UserMapper.ToEntity(request);

        foreach (var membershipHistory in request.MembershipHistory)
        {
            var membershipId = await unitOfWork.MembershipRepository.GetIdByStatus(MembershipStatus.Archive)
                ?? throw new MembershipNotFoundException(MembershipStatus.Archive);

            userToCreate.MembershipHistory.Add(new UserMembershipHistory
            {
                MembershipId = membershipId,
                ChangeDate = membershipHistory.ChangeDate,
                Description = membershipHistory.Description
            });
        }

        foreach (var history in request.RoleHistory)
        {
            var role = await roleService.GetByIdAsync(history.RoleId)
                ?? throw new UserRoleNotFoundException(history.RoleId);

            userToCreate.RoleHistory.Add(
                new UserRoleHistory
                {
                    RoleId = role.Id,
                    FunctionStartDate = history.FunctionStartDate,
                    FunctionEndDate = history.FunctionEndDate,
                    Description = history.Description
                }
            );
        }

        await unitOfWork.UserRepository.InsertAsync(userToCreate);
        await unitOfWork.SaveChangesAsync();

        if (!string.IsNullOrEmpty(request.Email) && request.CmsAdminAccess)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(userToCreate.Id, asTracking: true)
                ?? throw new NullReferenceException();

            userToCreate.ExternalId = await authProviderManagementService.CreateUser(
                request.Nickname,
                request.Email,
                user.RoleHistory.Where(x => x.FunctionEndDate == null).Select(x => x.Role!.ExternalId).ToArray());
        }

        return new BaseCreateResponse
        {
            Id = userToCreate.Id
        };
    }
}