using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class CircleLoader : ComponentBase
{
    [Parameter]
    public bool IsLoading { get; set; }
}