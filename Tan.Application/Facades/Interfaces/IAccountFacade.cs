using Tan.Application.Dtos;

namespace Tan.Application.Facades.Interfaces;

public interface IAccountFacade
{
    Task<ApiResponseDto<string>> GenerateTokenAsync(UserInfoDto filterDto, CancellationToken cancellationToken);
}