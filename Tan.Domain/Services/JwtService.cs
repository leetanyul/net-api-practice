using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Tan.Domain.Models;
using Tan.Domain.Services.Interfaces;

namespace Tan.Domain.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public async Task<string> GenerateTokenAsync(UserInfoFilter userInfo, CancellationToken cancellationToken, string existingToken = null)
    {
        if (!string.IsNullOrEmpty(existingToken))
        {
            // 기존 토큰이 있으면 토큰 검증 후 유효하면 기존 토큰 반환
            var principal = ValidateToken(existingToken);
            if (principal != null)
            {
                return existingToken;  // 기존 토큰이 유효하면 그대로 반환
            }
        }

        var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]);


        List<string> permissions = userInfo.Account.SubKey
            .Select(item => $"{item.SubKey}:{item.Grade.ToLower()}")
            .ToList();
        List<string> resultPermissions = new List<string>(permissions);

        // w권한일 경우 r도 있는것으로 간주
        foreach (var permission in permissions)
        {
            if (permission.EndsWith(":w"))
            {
                resultPermissions.Add(permission.Replace(":w", ":r"));
            }
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Account.AccountId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, userInfo.Account.Email),
            new Claim("accountStatus", userInfo.Account.Status),
            new Claim(ClaimTypes.Role, userInfo.Account.Role),
            new Claim("permissions", string.Join(",", resultPermissions)), //TODO: 카카오 kas response 값에 가공해서 넣기
            new Claim("originData", JsonSerializer.Serialize(userInfo))
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            // 토큰 검증
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            return principal;  // 유효한 토큰이라면 ClaimsPrincipal 반환
        }
        catch
        {
            return null;
        }
    }

    public string GetJwtTokenFromHeader(HttpContext context, CancellationToken cancellationToken)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].ToString();
        if (authorizationHeader.StartsWith("Bearer ", StringComparison.InvariantCultureIgnoreCase))
        {
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }
        return null;
    }
}
