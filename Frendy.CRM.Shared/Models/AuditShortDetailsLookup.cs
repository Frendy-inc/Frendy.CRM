using Frendy.Shared.Enums;
using Action = Frendy.Shared.Enums.Action;

namespace Frendy.CRM.Shared.Models;

public class AuditShortDetailsLookup
{
    public Guid Id { get; set; }
    public Guid ExecutorId { get; set; }
    public string ExecutorFirstName { get; set; } = null!;
    public string ExecutorLastName { get; set; } = null!;
    public UserRole ExecutorRole { get; set; }
    public Guid TargetId { get; set; }
    public string TargetFirstName { get; set; } = null!;
    public string TargetLastName { get; set; } = null!;
    public UserRole TargetRole { get; set; }
    public DateTime ActionDate { get; set; }
    public Action Action { get; set; }
}