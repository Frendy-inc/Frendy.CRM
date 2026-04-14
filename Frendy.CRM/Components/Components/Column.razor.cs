using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class Column : ComponentBase
{
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}