using Microsoft.AspNetCore.Authorization;

namespace Tan.Api.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public HashSet<string> RequiredPermissions { get; }

    public PermissionRequirement(params string[] permissions)
    {
        RequiredPermissions = permissions.ToHashSet();
    }
}

