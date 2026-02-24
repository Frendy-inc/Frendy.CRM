using Frendy.CRM.Services.Interfaces;
using Frendy.CRM.Shared.Extensions;
using Frendy.Shared.Enums;
using Frendy.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class Header: CustomComponentBase
{
    [Parameter] 
    public EventCallback OnToggleNavMenu { get; set; }
    
    [Parameter] 
    public EventCallback OnToggleUserMenu { get; set; }

    private bool _isUserMenuOpen;
    
    public string? FullName { get; set; }
    public string UserName { get; set; } = null!;
    public string Role { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var currentUser = await Client.GetCurrentUserDetailsAsync();
        
        FullName = currentUser.FullName ?? currentUser.UserName;
        UserName = currentUser.UserName;

        Role = currentUser.Role.GetLocalization(LocalizationService);
        
        StateHasChanged();
    }
    
    private async Task OnBurgerClicked()
    {
        if (OnToggleNavMenu.HasDelegate)
        {
            await OnToggleNavMenu.InvokeAsync();
        }
    }
    
    private async Task OnExpandClicked()
    {
        if (OnToggleUserMenu.HasDelegate)
        {
            _isUserMenuOpen = !_isUserMenuOpen;
            await OnToggleUserMenu.InvokeAsync();
        }
    }
}