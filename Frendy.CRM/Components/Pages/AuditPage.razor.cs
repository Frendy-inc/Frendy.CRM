using Frendy.CRM.Components.Components;
using Frendy.CRM.Components.Layout;
using Frendy.CRM.Services.Services;
using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Extensions;
using Frendy.CRM.Shared.Models;
using Frendy.Shared.Dto.RequestDto.AuditRequestDto;
using Frendy.Shared.Extensions;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Pages;

public partial class AuditPage: TableComponent<GetAuditsRequestDto, AuditShortDetails, AuditShortDetailsLookup>
{
    protected override async Task OnInitializedAsync()
    {
        HeadTitles.Add(LocalizationService.GetString("TABLE_ID_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_NAME_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_TARGET_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_ACTION_TIME_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_ACTION_TITLE"));
        
        await LoaderService.RunAsync(async () => await LoadAsync(new GetAuditsRequestDto
        {
            PageSize = PageSize,
            From = 0 
        }));
    }

    protected override async Task<AuditShortDetails> FetchDataAsync(GetAuditsRequestDto request) =>
        await Client.GetAuditsAsync(request);

    protected override Dictionary<string[], TableItemType> MapItem(AuditShortDetailsLookup item)
    {
        return new Dictionary<string[], TableItemType>
        {
            { [item.Id.ToString()], TableItemType.Text },
            {
                [
                    $"{item.ExecutorLastName} {item.ExecutorFirstName[0]}",
                    item.ExecutorRole.GetLocalization(LocalizationService)
                ], TableItemType.WithSubText
            },
            {
                [
                    $"{item.TargetLastName} {item.TargetFirstName[0]}",
                    item.TargetRole.GetLocalization(LocalizationService)
                ], TableItemType.WithSubText
            },
            { [$"{item.ActionDate.GetStringDate(withTime: true)}"], TableItemType.Text },
            {
                [
                    $"{item.Action.GetLocalization(LocalizationService)}",
                    item.Action.GetStyleClass()
                ], TableItemType.Status
            },
        };
    }

    protected override async Task OnPageChanged(int from)
    {
        await LoaderService.RunAsync(async () => await LoadAsync(new GetAuditsRequestDto
        {
            PageSize = PageSize, 
            From = from 
        }));
        
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnPageSizeChanged(int size)
    {
        await LoaderService.RunAsync(async () => await LoadAsync(new GetAuditsRequestDto
        {
            PageSize = size, 
            From = 0
        })); 
        
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnSearchValueChanged(string value)
    {
        await LoaderService.RunAsync(async () => await LoadAsync(new GetAuditsRequestDto
        {
            Search = value,
            PageSize = PageSize,
            From = 0 
        })); 
        
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OpenDialog(Guid guid)
    {
        var data = await LoaderService.RunAsync(async () => await Client.GetAuditDetailsAsync(guid));
        
        await ModalService.OpenAsync<AuditModal, bool>(new Dictionary<string, object>
        {
            ["Model"] = data,
            ["Title"] = LocalizationService.GetString("AUDIT_MODAL_TITLE")
        });
    }
}