using Frendy.CRM.Services.Interfaces;
using Frendy.CRM.Shared;
using Frendy.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components;

public abstract class CustomComponentBase: ComponentBase, IDisposable
{
    [Inject]
    public IClient Client { get; set; } = null!;
    
    [Inject]
    public ILocalizationService LocalizationService { get; set; } = null!;
    
    [Inject]
    public IJsRuntimeService JsRuntime { get; set; } = null!;
    
    [Inject]
    public AppState AppState { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        LocalizationService.LanguageChanged += OnLanguageChanged;
        
        await base.OnInitializedAsync();
    }

    protected virtual void OnLanguageChanged(object? sender, EventArgs e)
    {
        StateHasChanged();
    }
    
    public void Dispose()
    {
        LocalizationService.LanguageChanged -= OnLanguageChanged;
    }
}