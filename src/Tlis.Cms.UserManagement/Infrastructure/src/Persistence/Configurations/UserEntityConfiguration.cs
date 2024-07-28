using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Configurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.CmsAdminAccess).IsRequired();
        builder.Property(x => x.Firstname).IsRequired();
        builder.Property(x => x.Lastname).IsRequired();
        builder.Property(x => x.Nickname).IsRequired();
        builder.Property(x => x.Abouth).IsRequired();
        builder.Property(x => x.ProfileImageId);
        builder.Property(x => x.ExternalId);
        builder.Property(x => x.Email);

        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => new { x.Firstname, x.Lastname, x.Nickname }).IsUnique();
    }
}