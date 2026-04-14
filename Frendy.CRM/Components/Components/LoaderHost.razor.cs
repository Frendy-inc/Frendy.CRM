using Frendy.CRM.Services.Services;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class LoaderHost : ComponentBase
{
    [Inject]
    public LoaderService LoaderService { get; set; } = null!;
    private bool _isLoading;

    protected override void OnInitialized()
    {
        LoaderService.OnShow += Show;
        LoaderService.OnHide += Hide;
    }

    private void Show()
    {
        _isLoading = true;
        InvokeAsync(StateHasChanged);
    }

    private void Hide()
    {
        _isLoading = false;
        InvokeAsync(StateHasChanged);
    }
}