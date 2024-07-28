using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserDetailsGetResponseImage
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public required string Url { get; set; }
}