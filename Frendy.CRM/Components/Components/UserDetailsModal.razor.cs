using Frendy.CRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Frendy.CRM.Components.Components;

public partial class UserDetailsModal : CustomComponentBase
{
    [Parameter]
    public UserDetails Model { get; set; } = null!;

    private int Age { get; set; } = 0;
    
    private int GetAge()
    {
        var age = DateTime.Today.Year - Model.Birthday.Year;
    
        if (Model.Birthday.Date > DateTime.Today.AddYears(-age))
            age--;
        
        return age;
    }
    
    private string GetYearsText()
    {
        if (Age == 0)
            Age = GetAge();
            
        var lastDigit = Age % 10;
        var lastTwoDigits = Age % 100;
    
        if (lastTwoDigits is >= 11 and <= 14)
            return LocalizationService.GetString("USER_YEARS");
    
        return lastDigit switch
        {
            1 => LocalizationService.GetString("USER_YEAR"),
            2 or 3 or 4 => LocalizationService.GetString("USER_YEARS_SECOND_VARIANT"),
            _ => LocalizationService.GetString("USER_YEARS")
        };
    }
}