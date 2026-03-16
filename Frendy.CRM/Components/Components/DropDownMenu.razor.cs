using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class DropDownMenu : ComponentBase
{
    [Parameter] 
    public string Title { get; set; } = null!;
    
    [Parameter]
    public List<string>? Content { get; set; }
}