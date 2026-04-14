using Frendy.CRM.Services.Services;
using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class ModalHost : ComponentBase, IDisposable
{
    [Inject] 
    public ModalService ModalService { get; set; } = null!;

    private string? _title;
    private bool _isOpen;
    private Type? _componentType;
    private Dictionary<string, object>? _parameters;

    protected override void OnInitialized()
    {
        ModalService.OnOpen += Open;
        ModalService.OnClose += Close;
    }

    private void Open(ModalInstance instance)
    {
        _componentType = instance.ComponentType;

        _parameters = new Dictionary<string, object>(instance.Parameters);

        if (_parameters.TryGetValue("Title", out var title))
        {
            _title = title?.ToString();
            _parameters.Remove("Title");
        }
        else
        {
            _title = null;
        }

        _isOpen = true;
        InvokeAsync(StateHasChanged);
    }

    private void Close()
    {
        _isOpen = false;
        _componentType = null;
        _parameters = null;
        _title = null;

        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        ModalService.OnOpen -= Open;
        ModalService.OnClose -= Close;
    }
}