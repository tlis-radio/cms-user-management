using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.HttpServices.Base;
using Tlis.Cms.UserManagement.Infrastructure.HttpServices.Dtos;
using Tlis.Cms.UserManagement.Infrastructure.HttpServices.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.HttpServices;

internal sealed class ImageManagementHttpService(
    HttpClient client,
    IOptions<CmsServicesConfiguration> options)
    : BaseHttpService(client, options.Value.ImageAssetManagement), IImageManagementHttpService
{
    public Task<ImageDto> GetImageAsync(Guid id) => GetAsync<ImageDto>($"/image/{id}");
}