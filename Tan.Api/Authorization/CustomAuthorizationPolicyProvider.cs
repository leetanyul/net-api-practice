using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Tan.Api.Authorization;

/// <summary>
/// [Authorize(Policy = "account:r|log:r")] 과 같은 형태로 사용하며 or 연산으로 지원한다.
/// none 의 경우 login 된 상태이면 권한체크가 없는 것이며
/// 해당 어트리뷰트를 명시하지 않으면 login도 체크하지 않음
/// </summary>
public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions options;

    public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
        this.options = options.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (options.GetPolicy(policyName) is not null)
        {
            return await base.GetPolicyAsync(policyName);
        }
        //[Authorize(Policy = "account:r|log:r")] 형태로 사용
        // "account:r|log:r" 같은 정책을 만들 수 있도록 지원
        var requiredPermissions = policyName.Split('|');
        var policy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(requiredPermissions))
            .Build();

        this.options.AddPolicy(policyName, policy);
        return policy;
    }
}

