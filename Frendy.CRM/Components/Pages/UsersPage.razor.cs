using Frendy.CRM.Shared;
using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Extensions;
using Frendy.Shared.Dto.RequestDto.UserRequestDto;
using Frendy.Shared.Extensions;

namespace Frendy.CRM.Components.Pages;

public partial class UsersPage: CustomComponentBase
{
    public readonly List<string> HeadTitles = [];
    
    public List<Dictionary<string[], TableItemType>> Body { get; } = [];
    
    public int TotalCount { get; set; }

    public int PageSize = 20;
    
    public int CurrentPage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetUsersAsync(new GetUsersRequestDto
        {
            PageSize = PageSize,
            From = 0,
        });
        
        HeadTitles.Add(LocalizationService.GetString("TABLE_ID_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_NAME_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_LAST_ACTIVITY_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_PHONE_NUMBER_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_EMAIL_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_IS_ONLINE_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_IS_BANNED_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_AUTH_TYPE_TITLE"));
        
        await base.OnInitializedAsync();
    }

    public async Task OnPageChanged(int from)
    {
        await GetUsersAsync(new GetUsersRequestDto
        {
            PageSize = PageSize,
            From = from
        });
        StateHasChanged();
    }
    
    public async Task OnPageSizeChanged(int size)
    {
        await GetUsersAsync(new GetUsersRequestDto
        {
            PageSize = size,
            From = 0
        });
        StateHasChanged();
    }
    
    public async Task OnSearchValueChanged(string value)
    {
        await GetUsersAsync(new GetUsersRequestDto
        {
            Search = value,
            PageSize = PageSize,
            From = 0
        });
        StateHasChanged();
    }
    
    private async Task GetUsersAsync(GetUsersRequestDto requestDto)
    {
        var users = await Client.GetUsersAsync(requestDto);

        TotalCount = users.TotalCount;
        CurrentPage = users.Page;

        Body.Clear();
        foreach (var userDetail in users.Details)
        {
            Body.Add(new Dictionary<string[], TableItemType>
            {
                {
                    [userDetail.Id.ToString()],
                    TableItemType.Text
                },
                {
                    [
                        $"{userDetail.LastName} {userDetail.FirstName[0]}",
                        userDetail.Role.GetLocalization(LocalizationService)
                    ],
                    TableItemType.WithSubText
                },
                {
                    [userDetail.LastActivity.GetStringDate(withTime: true)],
                    TableItemType.Text
                },
                {
                    [!userDetail.PhoneNumber.IsNullOrEmpty() ? userDetail.PhoneNumber!.FormatPhone() 
                        : LocalizationService.GetString("TABLE_NO_SPECIFIED")],
                    TableItemType.Text
                },
                {
                    [userDetail.Email ?? LocalizationService.GetString("TABLE_NO_SPECIFIED")],
                    TableItemType.Text
                },
                {
                    [userDetail.IsBanned.ToString()],
                    TableItemType.Bool
                },
                {
                    [userDetail.IsOnline.ToString()],
                    TableItemType.Bool
                },
                {
                    [
                        $"{userDetail.AuthType.GetLocalization(LocalizationService)}",
                        userDetail.AuthType.GetStyleClass()
                    ],
                    TableItemType.Status
                }
            });
        }
        
        StateHasChanged();
    }

    private void ShowUserDetails(Guid userId)
    {
        AppState.CallUserDetails(userId);
    }
}