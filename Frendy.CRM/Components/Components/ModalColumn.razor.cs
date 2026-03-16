using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class ModalColumn : ComponentBase
{
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}