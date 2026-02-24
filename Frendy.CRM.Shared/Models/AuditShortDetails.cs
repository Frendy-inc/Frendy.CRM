namespace Frendy.CRM.Shared.Models;

public class AuditShortDetails
{
    public List<AuditShortDetailsLookup> Details { get; set; } = null!;
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}