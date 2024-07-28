using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserUpdateRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public bool CmsAdminAccess { get; set; }

    [SwaggerSchema(Description = "User's first name")]
    [JsonRequired]
    public string Firstname { get; set; } = null!;

    [SwaggerSchema(Description = "User's last name")]
    [JsonRequired]
    public string Lastname { get; set; } = null!;

    [SwaggerSchema(Description = "User's nickname or alias")]
    [JsonRequired]
    public string Nickname { get; set; } = null!;

    [SwaggerSchema(Description = "User's email address")]
    public string? Email { get; set; }

    [SwaggerSchema(Description = "User's description or bio")]
    [JsonRequired]
    public string Abouth { get; set; } = null!;

    [JsonRequired]
    public bool PreferNicknameOverName { get; set; }

    [JsonRequired]
    public List<UserUpdateRequestRoleHistory> RoleHistory { get; set; } = [];

    [JsonRequired]
    public List<UserUpdateRequestMembershipHistory> MembershipHistory { get; set; } = [];
}