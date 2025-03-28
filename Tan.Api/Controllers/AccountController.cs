using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Tan.Application.Dtos;
using Tan.Application.Facades.Interfaces;
using Tan.Domain.Services;

namespace Tan.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController(
    ILogger<AccountController> logger,
    IAccountFacade accountFacade) : ControllerBase
{
    /// <summary>
    /// 실제 로그인을 위한 api 는 아니먀 토큰 발급 로직만 보여주는 sample
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("token")]
    public async Task<IActionResult> GetToken(CancellationToken cancellationToken)
    {
        try
        {
            string sampleUserData = "{\"timestamp\": 1708172215000,\"status\": 200,\"desc\": \"Success\",\"content\": {\"accountId\": \"1\",\"name\": \"test\",\"email\": \"test@test.com\",\"accessToken\": \"eyJhbGciOiJIUzI1NiIsInR5cC\",\"role\": \"2\",\"status\": \"U\",\"subKey\": [{\"subKey\": \"account\",\"grade\": \"w\"},{\"subKey\": \"sample\",\"grade\": \"r\"}]}}";

            var userInfo = JsonSerializer.Deserialize<UserInfoDto>(sampleUserData);

            var token = await accountFacade.GenerateTokenAsync(userInfo, cancellationToken);

            return Ok(ApiResponseDto<string>.Success(token));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Token get");

            return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
        }
    }

}