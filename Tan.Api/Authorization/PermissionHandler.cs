using Microsoft.AspNetCore.Authorization;

namespace Tan.Api.Authorization;

public class PermissionHandler(IConfiguration configuration) : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var enableJwt = configuration["Jwt:enable"].ToString();
        if (enableJwt.Equals("n", StringComparison.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        // role 1이 admin 인 것으로 지정
        if (context.User.IsInRole("1"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userPermissions = context.User.Claims
                            .Where(c => c.Type == "permissions")
                            .SelectMany(c => c.Value.Split(','))
                            .ToHashSet();

        var expandedPermissions = new HashSet<string>(userPermissions);

        // api 의 대한 권한중 하나라도 있을 경우 succeed
        if (requirement.RequiredPermissions.Any(expandedPermissions.Contains))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}