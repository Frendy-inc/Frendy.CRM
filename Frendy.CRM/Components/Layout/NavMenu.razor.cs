using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class NavMenu: CustomComponentBase
{
    [Parameter]
    public bool IsCollapsed { get; set; }
}