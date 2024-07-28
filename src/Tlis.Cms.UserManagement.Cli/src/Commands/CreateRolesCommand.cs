using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tlis.Cms.UserManagement.Cli.Commands.Base;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Cli.Commands;

public class CreateRolesCommand(
    IUserManagementDbContext dbContext,
    IAuthProviderManagementService authProviderManagementService,
    ILogger<MigrationCommand> logger)
    : BaseCommand("create-roles", "Create user role entyties", logger)
{
    protected override async Task TryHandleCommand()
    {

        var authRoles = await authProviderManagementService.GetAllRoles();

        var roleEntites = new[]
        {
            new Role {
                Id = Guid.Parse("a9a9040c-fbbd-4aa6-b0dc-56de7265ee7f"),
                Name = UserRole.SystemAdmin,
                ExternalId = authRoles.First(x => x.Name == UserRole.SystemAdmin).Id
            },
            new Role {
                Id = Guid.Parse("cbec6f46-a2e8-4fb3-a126-fe4e51e5ead2"),
                Name = UserRole.Technician,
                ExternalId = authRoles.First(x => x.Name == UserRole.Technician).Id
            },
            new Role {
                Id = Guid.Parse("ed7cafb5-f2bf-4fbe-972c-18fa4f056b69"),
                Name = UserRole.Moderator,
                ExternalId = authRoles.First(x => x.Name == UserRole.Moderator).Id
            },
            new Role { 
                Id = Guid.Parse("fab1118e-38b9-4164-b222-66378654fcf4"),
                Name = UserRole.Graphic,
                ExternalId = authRoles.First(x => x.Name == UserRole.Graphic).Id
            },
            new Role {
                Id = Guid.Parse("8570d900-396f-4b78-bf69-5065e2fe8acf"),
                Name = UserRole.MarketingPr,
                ExternalId = authRoles.First(x => x.Name == UserRole.MarketingPr).Id
            },
            new Role {
                Id = Guid.Parse("4971ba3e-5a40-42cf-b9d9-17c49d9da309"),
                Name = UserRole.DramaturgeDj,
                ExternalId = authRoles.First(x => x.Name == UserRole.DramaturgeDj).Id
            },
            new Role {
                Id = Guid.Parse("f5bdf1df-8406-44d6-b1a1-942f7bde7b23"),

                Name = UserRole.WebDeveloper,
                ExternalId = authRoles.First(x => x.Name == UserRole.WebDeveloper).Id
            }
        };

        foreach (var role in roleEntites)
        {
            var exists = dbContext.Roles.Any(x => x.Id == role.Id);

            if (exists)
            {
                var toUpdate = await dbContext.Roles.AsTracking().FirstAsync(x => x.Id == role.Id);

                toUpdate.Name = role.Name;
                toUpdate.ExternalId = role.ExternalId;

                await dbContext.SaveChangesAsync();
            }
            else
            {
                await dbContext.Roles.AddAsync(role);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
