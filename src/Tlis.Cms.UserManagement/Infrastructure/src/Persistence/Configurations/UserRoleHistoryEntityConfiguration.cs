using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Configurations;

internal sealed class UserRoleHistoryEntityConfiguration : IEntityTypeConfiguration<UserRoleHistory>
{
    public void Configure(EntityTypeBuilder<UserRoleHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.RoleId).IsRequired();
        builder.Property(x => x.FunctionStartDate).IsRequired();
        builder.Property(x => x.FunctionEndDate);
        builder.Property(x => x.Description);

        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RoleId);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.RoleHistory)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}