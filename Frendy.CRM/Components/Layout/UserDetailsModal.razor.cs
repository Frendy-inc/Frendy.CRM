using Frendy.CRM.Shared;
using Frendy.CRM.Shared.Enums;
using Frendy.CRM.Shared.Extensions;
using Frendy.CRM.Shared.Models;
using Frendy.Shared.Extensions;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Layout;

public partial class UserDetailsModal : CustomComponentBase
{
    [Parameter]
    public Guid UserId { get; set; }
    
    private UserDetails? UserDetails { get; set; }
    private int Age { get; set; }
    private string AuthTypeStyle { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetUserDetailsAsync();

        AuthTypeStyle = $"status {UserDetails.AuthType.GetStyleClass()} bg";
        
        await base.OnInitializedAsync();
    }
    
    private async Task GetUserDetailsAsync()
    {
        var auditDetails = await Client.GetUserDetailsAsync(UserId);
        UserDetails = auditDetails;
        
        var age = DateTime.Today.Year - UserDetails.Birthday.Year;
    
        if (UserDetails.Birthday.Date > DateTime.Today.AddYears(-age))
            age--;
        
        Age = age;
    }
    
    private string GetYearsText(int years)
    {
        var lastDigit = years % 10;
        var lastTwoDigits = years % 100;
    
        if (lastTwoDigits is >= 11 and <= 14)
            return LocalizationService.GetString("USER_YEARS");
    
        return lastDigit switch
        {
            1 => LocalizationService.GetString("USER_YEAR"),
            2 or 3 or 4 => LocalizationService.GetString("USER_YEARS_SECOND_VARIANT"),
            _ => LocalizationService.GetString("USER_YEARS")
        };
    }

    private void Close()
    {
        AppState.CloseUserModal();
    }
}