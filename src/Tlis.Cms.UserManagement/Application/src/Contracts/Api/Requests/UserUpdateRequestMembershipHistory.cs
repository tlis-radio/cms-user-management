using System;
using System.Text.Json.Serialization;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserUpdateRequestMembershipHistory
{
    public Guid? Id { get; set; }

    [JsonRequired]
    public Guid MembershipId { get; set; }

    public string? Description { get; set; }

    [JsonRequired]
    public DateTime ChangeDate { get; set; }
}