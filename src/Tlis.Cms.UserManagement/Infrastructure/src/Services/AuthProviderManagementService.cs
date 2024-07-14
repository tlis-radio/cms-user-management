using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Auth0.Core.Exceptions;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.Extensions.Options;
using RandomString4Net;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Services;

internal sealed class AuthProviderManagementService(
    ITokenProviderService tokenProviderService,
    HttpClient httpClient,
    IOptions<Auth0Configuration> configuration)
    : IAuthProviderManagementService
{
    private readonly IManagementConnection _managementConnection = new HttpClientManagementConnection(httpClient);

    private readonly string _domain = configuration.Value.Domain;

    public async ValueTask<string> CreateUser(string username, string email, string[] roleIds)
    {
        try
        {
            using var client = await GetApiClient();

            var response = await client.Users.CreateAsync(
                new UserCreateRequest
                {
                    Email = email,
                    UserName = username,
                    Password = RandomString.GetString(Types.ALPHABET_MIXEDCASE_WITH_SYMBOLS, 15),
                    Connection = "Username-Password-Authentication"
                }
            );
            
            await client.Users.AssignRolesAsync(
                response.UserId,
                new AssignRolesRequest { Roles = roleIds });

            return response.UserId;
        }
        catch (ErrorApiException ex)
        {
            throw ex.StatusCode switch
            {
                HttpStatusCode.Conflict => new AuthProviderUserAlreadyExistsException(ex.Message),
                HttpStatusCode.BadRequest => new AuthProviderBadRequestException(ex.Message),
                _ => new AuthProviderException(ex.Message)
            };
        }
    }

    public async ValueTask UpdateUserRoles(string id, string[] roleIds)
    {
        using var client = await GetApiClient();

        await client.Users.AssignRolesAsync(
            id,
            new AssignRolesRequest { Roles = roleIds });
    }


    public async Task DeleteUser(string id)
    {
        using var client = await GetApiClient();

        await client.Users.DeleteAsync(id);
    }

    public async Task<List<Role>> GetAllRoles()
    {
        using var client = await GetApiClient();

        var response = await client.Roles.GetAllAsync(new GetRolesRequest());

        return [.. response];
    }

    private async ValueTask<IManagementApiClient> GetApiClient() =>
        new ManagementApiClient(await tokenProviderService.GetAuth0AccessToken(), _domain, _managementConnection);
}