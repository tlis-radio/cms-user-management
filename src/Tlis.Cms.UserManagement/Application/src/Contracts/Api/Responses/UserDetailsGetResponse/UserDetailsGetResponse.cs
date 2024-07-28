using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserDetailsGetResponse
{
    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    public string Nickname { get; set; } = null!;

    [Required]
    public string Abouth { get; set; } = null!;

    public UserDetailsGetResponseImage? ProfileImage { get; set; }

    [Required]
    public bool PreferNicknameOverName { get; set; }

    [Required]
    public bool CmsAdminAccess { get; set; }

    public string? ExternalId { get; set; }

    public string? Email { get; set; }

    [Required]
    public List<UserDetailsGetResponseUserRoleHistory> RoleHistory { get; set; } = [];

    [Required]
    public List<UserDetailsGetResponseUserMembershipHistory> MembershipHistory { get; set; } = [];
}