using AutoMapper;
using Tan.Application.Dtos;
using Tan.Application.Facades.Interfaces;
using Tan.Domain.Models;
using Tan.Domain.Services;
using Tan.Domain.Services.Interfaces;

namespace Tan.Application.Facades;

public class AccountFacade(IJwtService jwtService, IMapper mapper) : IAccountFacade
{
    public async Task<ApiResponseDto<string>> GenerateTokenAsync(UserInfoDto filterDto,
        CancellationToken cancellationToken)
    {
        var filter = mapper.Map<UserInfoFilter>(filterDto);

        var result = await jwtService.GenerateTokenAsync(filter, cancellationToken);

        var responseDto = mapper.Map<ApiResponseDto<string>>(result);

        return responseDto;
    }
}
