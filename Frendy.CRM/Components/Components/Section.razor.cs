using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class Section : ComponentBase
{
    [Parameter]
    public string? Style { get; set; }
    
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}