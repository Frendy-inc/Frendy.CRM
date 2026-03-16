using Frendy.CRM.Shared;
using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class Table : CustomComponentBase
{
    [Parameter]
    public List<string> HeadTitles { get; set; } = null!;
    
    [Parameter]
    public List<Dictionary<string[], TableItemType>> Body { get; set; } = null!;
    
    [Parameter]
    public int TotalCount { get; set; }
    
    [Parameter]
    public int PageSize { get; set; }
    
    [Parameter]
    public int CurrentPage { get; set; }
    
    [Parameter]
    public EventCallback<int> OnPageChanged { get; set; }
    
    [Parameter]
    public EventCallback<int> OnPageSizeChanged { get; set; }
    
    [Parameter]
    public EventCallback<string> OnSearchValueChanged { get; set; }
    
    [Parameter]
    public EventCallback<Guid> OnItemClick { get; set; }
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private bool _isRowView = true;
    
    public int PagesCount { get; set; }

    protected override void OnInitialized()
    {
        PagesCount = (int)Math.Ceiling((double)TotalCount / PageSize);
    }
    
    private async Task HandleClickAsync(int pageNumber)
    {
        if (OnPageChanged.HasDelegate)
        {
            await OnPageChanged.InvokeAsync(pageNumber);
        }
    }

    private async Task OnToggleViewAsync()
    {
        _isRowView = !_isRowView;
        
        if (OnPageSizeChanged.HasDelegate)
            await OnPageSizeChanged.InvokeAsync(_isRowView ? 20 : 30);
        
        StateHasChanged();
    }
    
    private async Task OnSearchValueChangedAsync(string value)
    {
        if (OnSearchValueChanged.HasDelegate)
            await OnSearchValueChanged.InvokeAsync(value);
    }
    
    private async Task ShowItemAsync(Guid id)
    {
        if (OnItemClick.HasDelegate)
            await OnItemClick.InvokeAsync(id);
    }
    
}