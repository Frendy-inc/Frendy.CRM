using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Extensions;
using Frendy.Shared.Dto.RequestDto.AuditRequestDto;

namespace Frendy.CRM.Components.Pages;

public partial class AuditPage: CustomComponentBase
{
    public readonly List<string> HeadTitles = [];
    
    public List<Dictionary<string[], TableItemType>> Body { get; } = [];
    
    public int TotalCount { get; set; }

    public int PageSize = 20;
    
    public int CurrentPage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAuditAsync(new GetAuditsRequestDto
        {
            PageSize = PageSize,
            From = 0,
        });
        
        HeadTitles.Add(LocalizationService.GetString("TABLE_ID_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_NAME_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_TARGET_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_ACTION_TIME_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_ACTION_TITLE"));
        
        await base.OnInitializedAsync();
    }

    public async Task OnPageChanged(int from)
    {
        await GetAuditAsync(new GetAuditsRequestDto
        {
            PageSize = PageSize,
            From = from
        });
        StateHasChanged();
    }
    
    public async Task OnPageSizeChanged(int size)
    {
        await GetAuditAsync(new GetAuditsRequestDto
        {
            PageSize = size,
            From = 0
        });
        StateHasChanged();
    }
    
    public async Task OnSearchValueChanged(string value)
    {
        await GetAuditAsync(new GetAuditsRequestDto
        {
            Search = value,
            PageSize = PageSize,
            From = 0
        });
        StateHasChanged();
    }
    
    private async Task GetAuditAsync(GetAuditsRequestDto requestDto)
    {
        var audits = await Client.GetAuditsAsync(requestDto);

        TotalCount = audits.TotalCount;
        CurrentPage = audits.Page;

        Body.Clear();
        foreach (var auditDetail in audits.Details)
        {
            Body.Add(new Dictionary<string[], TableItemType>
            {
                {
                    [auditDetail.Id.ToString()],
                    TableItemType.Text
                },
                {
                    [
                        $"{auditDetail.ExecutorLastName} {auditDetail.ExecutorFirstName[0]}",
                        auditDetail.ExecutorRole.GetLocalization(LocalizationService)
                    ],
                    TableItemType.WithSubText
                },
                {
                    [
                        $"{auditDetail.TargetLastName} {auditDetail.TargetFirstName[0]}",
                        auditDetail.TargetRole.GetLocalization(LocalizationService)
                    ],
                    TableItemType.WithSubText
                },
                {
                    [$"{auditDetail.ActionDate:MM.dd.yyyy hh:mm}"],
                    TableItemType.Text
                },
                {
                    [
                        $"{auditDetail.Action.GetLocalization(LocalizationService)}",
                        auditDetail.Action.GetActionClass()
                    ],
                    TableItemType.Status
                },
            });
        }
        
        StateHasChanged();
    }
}