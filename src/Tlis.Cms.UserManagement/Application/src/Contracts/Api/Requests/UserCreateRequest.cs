using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserCreateRequest : IRequest<BaseCreateResponse>
{
    [SwaggerSchema(Description = "User's first name")]
    [Required]
    public required string Firstname { get; set; }

    [SwaggerSchema(Description = "User's last name")]
    [Required]
    public required string Lastname { get; set; }

    [SwaggerSchema(Description = "User's nickname or alias")]
    [Required]
    public required string Nickname { get; set; }

    [SwaggerSchema(Description = "If user prefers to show his nickname or name on main page")]
    [Required]
    public bool PreferNicknameOverName { get; set; }

    [SwaggerSchema(Description = "User's description or bio")]
    [Required]
    public required string Abouth { get; set; }

    [SwaggerSchema(Description = "User's email address")]
    public string? Email { get; set; }

    [Required]
    public bool CmsAdminAccess { get; set; }

    [SwaggerSchema(Description = "User's role history.")]
    [Required]
    public required List<UserRoleHistoryCreateRequest> RoleHistory { get; set; } = [];

    [SwaggerSchema(Description = "User's membership history.")]
    [Required]
    public required List<UserMembershipHistoryCreateRequest> MembershipHistory { get; set; } = []; 
}

public sealed class UserRoleHistoryCreateRequest
{
    [SwaggerSchema(Description = "Id of the role.")]
    [Required]
    public Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The starting date of when user started this position.")]
    [Required]
    public DateTime FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public DateTime? FunctionEndDate { get; set; }

    [SwaggerSchema(Description = "Description why this position was given to user.")]
    public string? Description { get; set; }
}

public sealed class UserMembershipHistoryCreateRequest
{
    public MembershipStatus Status { get; set; }

    public DateTime ChangeDate { get; set; }

    public string Description { get; set; } = null!;
}