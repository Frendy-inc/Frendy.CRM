using Frendy.CRM.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class Icon: CustomComponentBase
{
    [Parameter]
    public Icons? IconType { get; set; }
    
    [Parameter]
    public string? IconUrl { get; set; }
    
    [Parameter]
    public IconSize Size { get; set; }
    
    [Parameter]
    public string? Color { get; set; }
    
    [Parameter]
    public string? Style { get; set; }

    private string _style = null!;
    private string? _color;

    protected override void OnInitialized()
    {
        _style = Size switch
        {
            IconSize.S => "width: 1rem; height: 1rem;",
            IconSize.M => "width: 1.5rem; height: 1.5rem;",
            IconSize.L => "width: 2rem; height: 2rem;",
            IconSize.XL => "width: 2.5rem; height: 2.5rem;",
            IconSize.XXL => "width: 3rem; height: 3rem;",
            IconSize.XXXL => "width: 3.5rem; height: 3.5rem;",
            _ => "width: 1.5rem; height: 1.5rem;"
        };
        
        _style += Style ?? "";
        
        if (!string.IsNullOrEmpty(Color))
            _color = $"color:{Color};";
        else
            _color = "color: var(--text-color);";
        
        base.OnInitialized();
    }
}