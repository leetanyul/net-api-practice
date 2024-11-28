using Tan.Application.Dtos;

namespace Tan.Application.Facades.Interfaces;

public interface ISampleFacade
{
    Task<PaginationDto<SampleResponseDto>> GetListByFilterAsync(SampleFilterDto filterDto, CancellationToken cancellationToken);
}
