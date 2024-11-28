using AutoMapper;
using Tan.Application.Dtos;
using Tan.Application.Facades.Interfaces;
using Tan.Domain.Models;
using Tan.Domain.Services.Interfaces;

namespace Tan.Application.Facades;

public class SampleFacade(ISampleService customerService, IMapper mapper) : ISampleFacade
{
    public async Task<PaginationDto<SampleResponseDto>> GetListByFilterAsync(SampleFilterDto filterDto,
        CancellationToken cancellationToken)
    {
        var filter = mapper.Map<SampleFilter>(filterDto);

        var result = await customerService.GetListByFilterAsync(filter, cancellationToken);

        var paginationDto = mapper.Map<PaginationDto<SampleResponseDto>>(result);

        return paginationDto;
    }
}
