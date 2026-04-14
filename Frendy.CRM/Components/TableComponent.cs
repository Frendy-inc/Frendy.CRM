using Frendy.CRM.Services.Services;
using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components;

public abstract class TableComponent<TRequest, TResponse, TItem> : CustomComponentBase 
    where TItem : class, new() 
    where TResponse : TableDetails<TItem>
{
    [Inject]
    public ModalService ModalService { get; set; } = null!;
    
    public readonly List<string> HeadTitles = [];
    public List<Dictionary<string[], TableItemType>> Body { get; set; } = [];

    protected int TotalCount { get; private set; }
    protected int PageSize { get; set; } = 20;
    public int CurrentPage { get; set; }
    private int _loadVersion = 0;

    protected abstract Task<TResponse> FetchDataAsync(TRequest request);
    protected abstract Dictionary<string[], TableItemType> MapItem(TItem item);
    protected abstract Task OnPageChanged(int from);
    protected abstract Task OnPageSizeChanged(int size);
    protected abstract Task OnSearchValueChanged(string value);
    protected abstract Task OpenDialog(Guid guid);

    protected async Task LoadAsync(TRequest request)
    {
        var version = ++_loadVersion;

        var response = await FetchDataAsync(request);

        if (version != _loadVersion)
            return;

        TotalCount = response.TotalCount;
        CurrentPage = response.Page;
        PageSize = response.PageSize;

        Body = response.Details
            .Select(MapItem)
            .ToList();

        await InvokeAsync(StateHasChanged);
    }
}