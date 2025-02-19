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
    [HttpGet("token")]
    public async Task<ActionResult<ApiResponseDto<string>>> GetToken(CancellationToken cancellationToken)
    {
        try
        {
            //TODO: ��ū ���� �ؾ��ϳ� pass(�ܼ��ϰ� GetJwtTokenFromHeader ȣ���ؼ� Ȯ���ϴ� �۾����� ����)
            //string token = accountFacade.GetJwtTokenFromHeader(HttpContext);

            // �ܺ� ���� �α��� api �� ���� �������°����� ������
            string sampleUserData = "{\"timestamp\": 1708172215000,\"status\": 200,\"desc\": \"Success\",\"content\": {\"accountId\": \"1\",\"name\": \"test\",\"email\": \"test@test.com\",\"accessToken\": \"eyJhbGciOiJIUzI1NiIsInR5cC\",\"role\": \"2\",\"status\": \"U\",\"subKey\": [{\"subKey\": \"account\",\"grade\": \"w\"},{\"subKey\": \"sample\",\"grade\": \"r\"}]}}";
            var userInfo = JsonSerializer.Deserialize<UserInfoDto>(sampleUserData);

            var result = await accountFacade.GenerateTokenAsync(userInfo, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Token get ex Activity Id : {activityId}",
                Activity.Current?.Id);

            return BadRequest(ex.Message);
        }
    }
}
