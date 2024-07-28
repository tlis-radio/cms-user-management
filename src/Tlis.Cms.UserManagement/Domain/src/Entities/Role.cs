using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;

    public string ExternalId { get; set; } = null!;
}