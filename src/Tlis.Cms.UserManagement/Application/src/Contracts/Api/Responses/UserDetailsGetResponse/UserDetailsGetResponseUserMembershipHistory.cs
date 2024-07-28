using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserDetailsGetResponseUserMembershipHistory
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public UserDetailsGetResponseUserMembershipHistoryMembership Membership { get; set; } = null!;

    [Required]
    public DateTime ChangeDate { get; set; }

    public string? Description { get; set; }
}