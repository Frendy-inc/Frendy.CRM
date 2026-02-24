using Frendy.Shared.Enums;
using Frendy.Shared.Interfaces;
using Action = Frendy.Shared.Enums.Action;

namespace Frendy.CRM.Shared.Extensions;

public static class EnumExtensions
{
    public static string GetLocalization(this UserRole role, ILocalizationService localizationService)
    {
        return role switch
        {
            UserRole.User => localizationService.GetString("HEADER_USER_ROLE"),
            UserRole.Administrator => localizationService.GetString("HEADER_ADMIN_ROLE"),
            UserRole.Moderator => localizationService.GetString("HEADER_MODER_ROLE"),
            UserRole.Support => localizationService.GetString("HEADER_SUPPORT_ROLE"),
            _ => role.ToString()
        };
    }

    public static string GetLocalization(this Action action, ILocalizationService localizationService)
    {
        return action switch
        {
            Action.Registration => localizationService.GetString("TABLE_REGISTRATION_ACTION"),
            Action.Authorization => localizationService.GetString("TABLE_AUTHORIZATION_ACTION"),
            Action.BlockingTheUser => localizationService.GetString("TABLE_BLOCKING_ACTION"),
            Action.UnblockingTheUser => localizationService.GetString("TABLE_UNBLOCKING_ACTION"),
            Action.PasswordChange => localizationService.GetString("TABLE_PASS_CHANGE_ACTION"),
            Action.ProfileUpdate => localizationService.GetString("TABLE_PROFILE_UPDATE_ACTION"),
            Action.GettingUserList => localizationService.GetString("TABLE_GET_USER_LIST_ACTION"),
            Action.ChangingTheUsersRole => localizationService.GetString("TABLE_CHANGE_ROLE_ACTION"),
            Action.AddingNewAction => localizationService.GetString("TABLE_ADD_NEW_ACTION"),
            Action.ActionUpdate => localizationService.GetString("TABLE_UPDATE_ACTION"),
            _ => action.ToString()
        };
    }

    public static string GetActionClass(this Action action)
    {
        return action switch
        {
            Action.Registration => "positive",
            Action.Authorization => "positive",
            Action.BlockingTheUser => "negative",
            Action.UnblockingTheUser => "neutral",
            Action.PasswordChange => "neutral",
            Action.ProfileUpdate => "neutral",
            Action.GettingUserList => "positive",
            Action.ChangingTheUsersRole => "neutral",
            Action.AddingNewAction => "positive",
            Action.ActionUpdate => "neutral",
            _ => action.ToString()
        };
    }
}