using System;
using System.Collections.Generic;
using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class User : BaseEntity
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string Abouth { get; set; } = null!;

    public string? ProfileImageUrl { get; set; }

    public bool PreferNicknameOverName { get; set; }

    public bool IsActive { get; set; }

    public string? ExternalId { get; set; }

    public string? Email { get; set; }
    
    public virtual ICollection<UserRoleHistory> RoleHistory { get; set; } = [];

    public virtual ICollection<UserMembershipHistory> MembershipHistory { get; set; } = [];
}