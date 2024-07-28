using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserPaginationGetResponse
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public bool CmsAdminAccess { get; set; }

    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    public string Nickname { get; set; } = null!;

    public string? Email { get; set; }

    public List<string> Roles { get; set; } = [];

    public string? Status { get; set; } = null;
}