using Frendy.CRM.Components.Components;
using Frendy.CRM.Components.Layout;
using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Extensions;
using Frendy.CRM.Shared.Models;
using Frendy.Shared.Dto.RequestDto.UserRequestDto;
using Frendy.Shared.Extensions;

namespace Frendy.CRM.Components.Pages;

public partial class UsersPage: TableComponent<GetUsersRequestDto, UserShortDetails, UserShortDetailsLookup>
{
    protected override async Task OnInitializedAsync()
    {
        HeadTitles.Add(LocalizationService.GetString("TABLE_ID_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("TABLE_NAME_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_LAST_ACTIVITY_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_PHONE_NUMBER_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_EMAIL_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_IS_ONLINE_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_IS_BANNED_TITLE"));
        HeadTitles.Add(LocalizationService.GetString("USER_AUTH_TYPE_TITLE"));
        
        await LoaderService.RunAsync(async () => await LoadAsync(new GetUsersRequestDto
        {
            PageSize = PageSize,
            From = 0
        }));
    }
    
    protected override Task<UserShortDetails> FetchDataAsync(GetUsersRequestDto request)
        => Client.GetUsersAsync(request);

    protected override Dictionary<string[], TableItemType> MapItem(UserShortDetailsLookup item)
    {
        return new Dictionary<string[], TableItemType>
        {
            {
                [item.Id.ToString()],
                TableItemType.Text
            },
            {
                [
                    $"{item.LastName} {item.FirstName[0]}",
                    item.Role.GetLocalization(LocalizationService)
                ],
                TableItemType.WithSubText
            },
            {
                [item.LastActivity.GetStringDate(withTime: true)],
                TableItemType.Text
            },
            {
                [!item.PhoneNumber.IsNullOrEmpty() ? item.PhoneNumber!.FormatPhone() 
                    : LocalizationService.GetString("TABLE_NO_SPECIFIED")],
                TableItemType.Text
            },
            {
                [item.Email ?? LocalizationService.GetString("TABLE_NO_SPECIFIED")],
                TableItemType.Text
            },
            {
                [item.IsBanned.ToString()],
                TableItemType.Bool
            },
            {
                [item.IsOnline.ToString()],
                TableItemType.Bool
            },
            {
                [
                    $"{item.AuthType.GetLocalization(LocalizationService)}",
                    item.AuthType.GetStyleClass()
                ],
                TableItemType.Status
            }
        };
    }
    protected override async Task OnPageChanged(int from)
    {
        await LoaderService.RunAsync(async () => await LoadAsync(new GetUsersRequestDto
        {
            PageSize = PageSize,
            From = from
        }));
        
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnPageSizeChanged(int size)
    {
        await LoaderService.RunAsync(async () => await LoadAsync(new GetUsersRequestDto
        {
            PageSize = size, 
            From = 0
        })); 
        
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnSearchValueChanged(string value)
    {
        await LoaderService.RunAsync(async () => await LoadAsync(new GetUsersRequestDto
        {
            Search = value,
            PageSize = PageSize,
            From = 0 
        })); 
        
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OpenDialog(Guid guid)
    {
        var data = await LoaderService.RunAsync(async () => await Client.GetUserDetailsAsync(guid));
        
        await ModalService.OpenAsync<UserDetailsModal, bool>(new Dictionary<string, object>
        {
            ["Model"] = data,
            ["Title"] = LocalizationService.GetString("USER_MODAL_TITLE")
        });
    }
}