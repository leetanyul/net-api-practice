using Tan.Application.Dtos;

namespace Tan.Application.Facades.Interfaces;

public interface IAccountFacade
{
    Task<string> GenerateTokenAsync(UserInfoDto filterDto, CancellationToken cancellationToken);
}