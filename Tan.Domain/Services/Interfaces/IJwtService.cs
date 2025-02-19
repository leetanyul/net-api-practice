using Microsoft.AspNetCore.Http;
using Tan.Domain.Models;

namespace Tan.Domain.Services.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(UserInfoFilter userInfo, CancellationToken cancellationToken, string existingToken = null);
    string GetJwtTokenFromHeader(HttpContext context, CancellationToken cancellationToken);
}