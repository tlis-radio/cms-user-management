using System;
using System.Linq;
using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.HttpServices.Dtos;

namespace Tlis.Cms.UserManagement.Application.Mappers;

[Mapper]
internal static partial class UserMapper
{
    public static UserPaginationGetResponse ToPaginationDto(User entity)
    {
        var resposne = MapToPaginationDto(entity);

        resposne.Roles = entity.RoleHistory.Select(x => x.Role!.Name).ToList();

        var latestMembership = entity.MembershipHistory.OrderByDescending(x => x.ChangeDate).FirstOrDefault();

        if (latestMembership != null && latestMembership.Membership != null)
        {
            resposne.Status = Enum.GetName(latestMembership.Membership.Status);
        }

        return resposne;
    }

    [MapperIgnoreSource(nameof(User.ExternalId))]
    [MapperIgnoreSource(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(User.Abouth))]
    [MapperIgnoreSource(nameof(User.ProfileImageId))]
    [MapperIgnoreSource(nameof(User.PreferNicknameOverName))]
    public static partial UserFilterGetResponse ToFilterDto(User entity);

    public static UserDetailsGetResponse? ToDto(User? entity, ImageDto? image)
    {
        if (entity == null)
        {
            return null;
        }

        var response = new UserDetailsGetResponse
        {
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Nickname = entity.Nickname,
            PreferNicknameOverName = entity.PreferNicknameOverName,
            CmsAdminAccess = entity.CmsAdminAccess,
            Abouth = entity.Abouth,
            ExternalId = entity.ExternalId,
            MembershipHistory = entity.MembershipHistory.Select(MapToUserDetailsGetResponseUserMembershipHistory).ToList(),
            RoleHistory = entity.RoleHistory.Select(MapToUserDetailsGetResponseUserRoleHistory).ToList()
        };

        if (image != null)
        {
            response.ProfileImage = new UserDetailsGetResponseImage
            {
                Id = image.Id,
                Url = image.Url
            };
        }

        return response;
    }
    
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.ProfileImageId))]
    [MapperIgnoreTarget(nameof(User.ExternalId))]
    [MapperIgnoreTarget(nameof(User.MembershipHistory))]
    [MapperIgnoreTarget(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(UserCreateRequest.MembershipHistory))]
    [MapperIgnoreSource(nameof(UserCreateRequest.RoleHistory))]
    public static partial User ToEntity(UserCreateRequest dto);
    
    [MapperIgnoreTarget(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.User))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Role))]
    public static partial UserRoleHistory ToEntity(UserUpdateRequestRoleHistory dto);

    public static UserRoleHistory ToExistingEntity(UserRoleHistory existing, UserUpdateRequestRoleHistory dto)
    {
        existing.RoleId = dto.RoleId;
        existing.FunctionEndDate = dto.FunctionEndDate;
        existing.FunctionStartDate = dto.FunctionStartDate;
        existing.Description = dto.Description;

        return existing;
    }

    [MapperIgnoreTarget(nameof(UserMembershipHistory.UserId))]
    [MapperIgnoreTarget(nameof(UserMembershipHistory.Membership))]
    public static partial UserMembershipHistory ToEntity(UserUpdateRequestMembershipHistory dto);

    public static UserMembershipHistory ToExistingEntity(UserMembershipHistory existing, UserUpdateRequestMembershipHistory dto)
    {
        existing.MembershipId = dto.MembershipId;
        existing.ChangeDate = dto.ChangeDate;
        existing.Description = dto.Description;

        return existing;
    }

    [MapProperty(nameof(UserRoleHistoryCreateRequest.RoleId), nameof(UserRoleHistory.RoleId))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.User))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Role))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Id))]
    private static partial UserRoleHistory MapToUserRoleHistory(UserRoleHistoryCreateRequest dto);

    [MapperIgnoreSource(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.RoleId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.User))]
    private static partial UserDetailsGetResponseUserRoleHistory MapToUserDetailsGetResponseUserRoleHistory(UserRoleHistory entity);

    [MapperIgnoreSource(nameof(Role.ExternalId))]
    private static partial UserDetailsGetResponseRole MapToUserDetailsGetResponseRole(Role role);
    
    [MapperIgnoreSource(nameof(UserMembershipHistory.UserId))]
    [MapperIgnoreSource(nameof(UserMembershipHistory.MembershipId))]
    private static partial UserDetailsGetResponseUserMembershipHistory MapToUserDetailsGetResponseUserMembershipHistory(UserMembershipHistory entity);

    [MapperIgnoreSource(nameof(User.ExternalId))]
    [MapperIgnoreSource(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(User.Abouth))]
    [MapperIgnoreSource(nameof(User.ProfileImageId))]
    [MapperIgnoreSource(nameof(User.PreferNicknameOverName))]
    [MapperIgnoreTarget(nameof(UserPaginationGetResponse.Status))]
    [MapperIgnoreTarget(nameof(UserPaginationGetResponse.Roles))]
    private static partial UserPaginationGetResponse MapToPaginationDto(User entity);
}