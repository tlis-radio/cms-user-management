using System;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Infrastructure.HttpServices.Dtos;

namespace Tlis.Cms.UserManagement.Infrastructure.HttpServices.Interfaces;

public interface IImageManagementHttpService
{
    Task<ImageDto> GetImageAsync(Guid id);
}