using Microsoft.AspNetCore.Mvc.Filters;

namespace Tan.Api.Attributes.Authorizations;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            // 익명접근이 가능한 경우 return 해서 controller 로 진입
            return;
        }

        var allowAdmin = context.ActionDescriptor.EndpointMetadata.OfType<AllowAdminAttribute>().Any();
        // 관리자 권한이 필요할때는 관리자인지 확인
        if (allowAdmin)
        {
            // todo : 여기서 관리자 체크하고 관리자가 아닐 경우 context.Result 를 넣어서 controller 진입 하지 못하고 api 응답 처리
            // 관리자 일 경우는 진행해서 controller 진입
            return;
        }
    }
}
