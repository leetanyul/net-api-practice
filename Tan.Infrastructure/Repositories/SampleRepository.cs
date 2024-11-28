using Tan.Domain.Models;
using Tan.Domain.Repositories;
using Tan.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Tan.Infrastructure.Repositories;

public class SampleRepository(SampleContext context) : RepositoryBase<Domain.Entities.Sample>(context), ISampleRepository
{
    public async Task<int> CountByFilterAsync(SampleFilter filter, CancellationToken cancellationToken)
    {
        var query = DbContext.Customers.AsQueryable();

        query = ApplyFilter(filter, query);

        return await query.CountAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.Sample>> GetListByFilterAsync(SampleFilter filter,
        CancellationToken cancellationToken)
    {
        var query = DbContext.Customers.AsQueryable();

        query = ApplyFilter(filter, query);

        query = ApplySorting(filter, query);

        if (filter.CurrentPage > 0)
            query = query.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize);

        return await query.ToListAsync(cancellationToken);
    }

    private static IQueryable<Domain.Entities.Sample> ApplySorting(SampleFilter filter,
        IQueryable<Domain.Entities.Sample> query)
    {
        query = filter?.OrderBy.ToLower() switch
        {
            "name" => filter.SortBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? query.OrderBy(x => x.Name)
                : query.OrderByDescending(x => x.Name),
            "description" => filter.SortBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? query.OrderBy(x => x.Description)
                : query.OrderByDescending(x => x.Description),
            "id" => filter.SortBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? query.OrderBy(x => x.Id)
                : query.OrderByDescending(x => x.Id),
            _ => query
        };

        return query;
    }

    private static IQueryable<Domain.Entities.Sample> ApplyFilter(SampleFilter filter,
        IQueryable<Domain.Entities.Sample> query)
    {
        if (filter.Id > 0)
            query = query.Where(x => x.Id == filter.Id);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));

        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(x => EF.Functions.Like(x.Description, $"%{filter.Description}%"));

        return query;
    }
}