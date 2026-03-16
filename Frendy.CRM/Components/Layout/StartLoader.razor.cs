using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class StartLoader : ComponentBase
{
    [Parameter]
    public bool IsLoading { get; set; }
}