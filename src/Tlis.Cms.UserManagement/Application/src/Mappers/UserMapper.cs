using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Application.Mappers;

[Mapper]
internal static partial class UserMapper
{
    public static User ToEntity(UserCreateRequest dto)
    {
        var entity = MapToUser(dto);

        entity.IsActive = true;
        
        return entity;
    }

    [MapperIgnoreSource(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(UserDetailsGetResponse.MembershipEndedDate))]
    [MapperIgnoreTarget(nameof(UserDetailsGetResponse.MembershipEndedReason))]
    [MapperIgnoreTarget(nameof(UserDetailsGetResponse.MemberSinceDate))]
    public static partial UserDetailsGetResponse? ToDto(User? entity);
    
    [MapperIgnoreSource(nameof(User.ExternalId))]
    [MapperIgnoreSource(nameof(User.MembershipHistory))]
    [MapperIgnoreTarget(nameof(UserPaginationGetResponse.MembershipEndedDate))]
    [MapperIgnoreTarget(nameof(UserPaginationGetResponse.MembershipEndedReason))]
    [MapperIgnoreTarget(nameof(UserPaginationGetResponse.MemberSinceDate))]
    public static partial UserPaginationGetResponse ToPaginationDto(User entity);
    
    [MapperIgnoreTarget(nameof(User.IsActive))]
    [MapperIgnoreTarget(nameof(User.ProfileImageUrl))]
    [MapperIgnoreTarget(nameof(User.ExternalId))]
    [MapperIgnoreTarget(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(ArchiveUserCreateRequest.Password))]
    [MapperIgnoreSource(nameof(ArchiveUserCreateRequest.MembershipHistory))]
    public static partial User ToEntity(ArchiveUserCreateRequest dto);
    
    [MapperIgnoreTarget(nameof(User.IsActive))]
    [MapperIgnoreTarget(nameof(User.ProfileImageUrl))]
    [MapperIgnoreTarget(nameof(User.ExternalId))]
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.RoleHistory))]
    [MapperIgnoreTarget(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(ArchiveUserCreateRequest.Password))]
    [MapperIgnoreSource(nameof(UserCreateRequest.RoleId))]
    [MapperIgnoreSource(nameof(UserCreateRequest.FunctionStartDate))]
    [MapperIgnoreSource(nameof(UserCreateRequest.MemberSinceDate))]
    private static partial User MapToUser(UserCreateRequest dto);

    [MapProperty(nameof(ArchiveUserRoleHistoryCreateRequest.RoleId), nameof(UserRoleHistory.RoleId))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.User))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Role))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Id))]
    private static partial UserRoleHistory MapToUserRoleHistory(ArchiveUserRoleHistoryCreateRequest dto);

    [MapperIgnoreSource(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.RoleId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.User))]
    [MapperIgnoreSource(nameof(UserRoleHistory.Id))]
    private static partial UserDetailsGetResponseUserRoleHistory MapToUserDetailsGetResponseUserRoleHistory(UserRoleHistory entity);

    [MapperIgnoreSource(nameof(Role.Id))]
    private static partial UserDetailsGetResponseRole MapToUserDetailsGetResponseRole(Role role);
    
    [MapperIgnoreSource(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.RoleId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.User))]
    [MapperIgnoreSource(nameof(UserRoleHistory.Id))]
    private static partial UserPaginationGetResponseUserRoleHistory MapToUserPaginationGetResponseUserRoleHistory(UserRoleHistory entity);

    [MapperIgnoreSource(nameof(Role.Id))]
    private static partial UserPaginationGetResponseRole MapToUserPaginationGetResponseRole(Role role);

    [MapperIgnoreSource(nameof(UserMembershipHistory.UserId))]
    [MapperIgnoreSource(nameof(UserMembershipHistory.Id))]
    private static partial UserDetailsGetResponseUserMembershipHistory MapToUserDetailsGetResponseUserMembershipHistory(UserMembershipHistory entity);
}