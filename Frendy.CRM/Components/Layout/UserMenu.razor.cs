using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class UserMenu: CustomComponentBase
{
    [Parameter]
    public bool IsCollapsed { get; set; }
    
    private bool _isRussian = true;
    private bool _isLightTheme = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var currentCulture = CultureInfo.CurrentCulture;
        
        _isRussian = currentCulture.TwoLetterISOLanguageName == "ru";
        var currentTheme = await JsRuntime.GetLocalStorageAsync<string>("currentTheme");
        _isLightTheme = currentTheme == "light";
        
        await base.OnAfterRenderAsync(firstRender);
    }
    
    private async Task ToggleLanguageAsync(bool isRussian)
    {
        _isRussian = isRussian;
        var currentCulture = _isRussian ? CultureInfo.GetCultureInfo("ru") : CultureInfo.GetCultureInfo("en");
        await LocalizationService.SetCurrentCultureAsync(currentCulture);
        
        await JsRuntime.SetLocalStorageAsync("currentCulture", currentCulture.Name);
        await JsRuntime.InvokeVoidAsync("location.reload");
    }

    private async Task ToggleThemeAsync(bool isLight)
    {
        _isLightTheme = isLight;
        var theme = isLight ? "light" : "dark";
        await JsRuntime.InvokeVoidAsync("document.documentElement.setAttribute", "data-theme", theme);
        await JsRuntime.SetLocalStorageAsync("currentTheme", theme);
        StateHasChanged();
    }

}