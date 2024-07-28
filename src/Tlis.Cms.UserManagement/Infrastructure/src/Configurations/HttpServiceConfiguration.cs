using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Configurations;

internal sealed class HttpServiceConfiguration
{
    public required Uri BaseAddress { get; set; }
}