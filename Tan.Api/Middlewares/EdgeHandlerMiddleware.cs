namespace Tan.Api.Middlewares;

/// <summary>
/// 서비스 제일 외곽에 위치하는 미들웨어로 api 요청에 해당 request 롸 response logging 및 예외 처리 용으로 사용
/// </summary>
public class EdgeHandlerMiddleware(
    ILogger<EdgeHandlerMiddleware> logger,
    RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // todo: request logging
            await next(context);
            // todo: response logging
        }
        catch (Exception ex)
        {
        }
    }
}

public static class EdgeHandlerMiddlewareExtenions
{
    public static IApplicationBuilder UseEdgeHandlerMiddleware(this IApplicationBuilder builer)
    {
        return builer.UseMiddleware<EdgeHandlerMiddleware>();
    }
}