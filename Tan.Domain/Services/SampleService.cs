using System.ComponentModel.DataAnnotations;
using Tan.Domain.Models;
using Tan.Domain.Repositories;
using Tan.Domain.Services.Interfaces;

namespace Tan.Domain.Services;

public class SampleService(ISampleRepository customerRepository) : ISampleService
{
    public async Task<Pagination<Entities.Sample>> GetListByFilterAsync(SampleFilter filter, CancellationToken cancellationToken)
    {
        if (filter is null)
            throw new ValidationException("filter is null.");

        if (filter.PageSize > 100)
            throw new ValidationException("page size over (max : 100)");

        if (filter.CurrentPage <= 0) filter.PageSize = 1;

        var total = await customerRepository.CountByFilterAsync(filter, cancellationToken);

        if (total == 0) return new Pagination<Entities.Sample>();

        var paginateResult = await customerRepository.GetListByFilterAsync(filter, cancellationToken);

        var result = new Pagination<Entities.Sample>
        {
            Count = total,
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            Result = [.. paginateResult]
        };

        return result;
    }
}
