using Tan.Api.Utils;

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
            // log에 리퀘스트 추척 id 남기기
            LoggingContext.TraceId = context.TraceIdentifier;
            logger.LogInformation("test");
            // todo: request logging
            await next(context);
            // todo: response logging
        }
        catch (Exception ex)
        {
        }
        finally
        {
            LoggingContext.Clear();
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