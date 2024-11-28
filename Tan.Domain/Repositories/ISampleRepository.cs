using Tan.Domain.Models;

namespace Tan.Domain.Repositories;

public interface ISampleRepository : IRepositoryBase<Entities.Sample>
{
    Task<int> CountByFilterAsync(SampleFilter filter, CancellationToken cancellationToken);
    Task<List<Entities.Sample>> GetListByFilterAsync(SampleFilter filter, CancellationToken cancellationToken);
}
