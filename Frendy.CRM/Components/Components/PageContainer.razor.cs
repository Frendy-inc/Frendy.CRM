using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class PageContainer : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}