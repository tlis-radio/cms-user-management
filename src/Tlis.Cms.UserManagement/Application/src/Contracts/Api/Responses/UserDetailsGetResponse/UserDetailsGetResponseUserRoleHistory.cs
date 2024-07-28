using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserDetailsGetResponseUserRoleHistory
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime FunctionStartDate { get; set; }

    public DateTime? FunctionEndDate { get; set; }

    [Required]
    public UserDetailsGetResponseRole? Role { get; set; }

    public string? Description { get; set; }
}