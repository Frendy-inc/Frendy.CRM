using System.Globalization;
using Frendy.CRM.Services.Interfaces;
using Frendy.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class MainLayout : LayoutComponentBase
{
    [Inject]
    public ILocalizationService LocalizationService { get; set; } = null!;
    
    [Inject]
    private IJsRuntimeService JsRuntime { get; set; } = null!;
    
    private bool _navMenuIsCollapsed;
    private bool _userMenuIsCollapsed = true;
    private bool _isLoading = true;
    private bool _themeIsApplied;

    private Task ToggleNavMenuState()
    {
        _navMenuIsCollapsed = !_navMenuIsCollapsed;
        return Task.CompletedTask;
    }
    
    private Task ToggleUserMenuState()
    {
        _userMenuIsCollapsed = !_userMenuIsCollapsed;
        return Task.CompletedTask;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_themeIsApplied)
        {
            var currentTheme = await JsRuntime.GetLocalStorageAsync<string>("currentTheme");
            var isLightTheme = currentTheme == "light";
            var theme = isLightTheme ? "light" : "dark";
            await JsRuntime.InvokeVoidAsync("document.documentElement.setAttribute", "data-theme", theme);
            await JsRuntime.SetLocalStorageAsync("currentTheme", theme);
            
            StateHasChanged();
            _themeIsApplied = true;
        }
            
        var currentCulture = await JsRuntime.GetLocalStorageAsync<string>("currentCulture");
        if (currentCulture is not null)
        {
            await LocalizationService.SetCurrentCultureAsync(CultureInfo.GetCultureInfo(currentCulture));
        }
        StateHasChanged();
        
        _isLoading = false;
        
        await base.OnAfterRenderAsync(firstRender);
    }
}