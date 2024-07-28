using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserUpdateProfileImageRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonRequired]
    public Guid ProfileImageId { get; set; }
}