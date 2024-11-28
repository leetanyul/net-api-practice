namespace Tan.Application.Dtos;

public record SampleFilterDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string OrderBy { get; set; } = "name";
    public string SortBy { get; set; } = "asc";
}
