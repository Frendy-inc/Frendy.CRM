using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class ModalRow : ComponentBase
{
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}