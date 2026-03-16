namespace Frendy.CRM.Shared.Models;

public abstract class TableDetails<T> where T : class, new()
{
    public List<T> Details { get; set; } = null!;
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}