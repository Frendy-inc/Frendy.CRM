using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class StartLoader : ComponentBase
{
    [Parameter]
    public bool IsLoading { get; set; }
}