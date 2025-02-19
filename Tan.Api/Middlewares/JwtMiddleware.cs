using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Tan.Application.Constants.Enum;
using Tan.Application.Dtos;

namespace Tan.Api.Middlewares;

public class JwtMiddleware(
    RequestDelegate next,
    ILogger<JwtMiddleware> logger,
    IConfiguration configuration)
{
    /// <summary>
    /// 토큰 만료 체크를 위한 미들웨어
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var enableJwt = configuration["Jwt:enable"].ToString();
        if (enableJwt.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var jwtToken = tokenHandler.ReadJwtToken(token);
                    var exp = jwtToken?.Payload?.FirstOrDefault(c => c.Key == "exp").Value?.ToString();

                    if (exp != null)
                    {
                        var expirationDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp));
                        if (expirationDate < DateTimeOffset.UtcNow)
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            var response = new ApiResponseDto<string>()
                            {
                                Code = (int)ResponseCode.TokenExpired,
                                Message = "Your login session has expired.",
                                Data = string.Empty,
                            };

                            await httpContext.Response.WriteAsJsonAsync(response);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error decoding JWT token");
                }
            }
        }            

        await next(httpContext);
    }
}

public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}
