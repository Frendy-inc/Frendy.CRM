using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class AuditModal : CustomComponentBase
{
    [Parameter]
    public AuditDetails Model { get; set; } = null!;
}