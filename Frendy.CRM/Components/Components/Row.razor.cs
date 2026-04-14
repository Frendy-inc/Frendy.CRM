using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class Row : ComponentBase
{
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}