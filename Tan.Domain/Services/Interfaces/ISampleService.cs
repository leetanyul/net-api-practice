using Tan.Domain.Models;

namespace Tan.Domain.Services.Interfaces;

public interface ISampleService
{
    Task<Pagination<Entities.Sample>> GetListByFilterAsync(SampleFilter filter, CancellationToken cancellationToken);
}
