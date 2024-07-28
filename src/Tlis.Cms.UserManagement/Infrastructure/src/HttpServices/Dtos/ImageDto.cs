using System;
using System.Text.Json.Serialization;

namespace Tlis.Cms.UserManagement.Infrastructure.HttpServices.Dtos;

public sealed class ImageDto
{
    [JsonRequired]
    public Guid Id { get; set; }

    [JsonRequired]
    public int Width { get; set; }

    [JsonRequired]
    public int Height { get; set; }

    [JsonRequired]
    public required string Url { get; set; }
}