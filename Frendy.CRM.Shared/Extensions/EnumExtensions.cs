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
            UserRole.User => localizationService.GetString("ROLE_USER"),
            UserRole.Administrator => localizationService.GetString("ROLE_ADMIN"),
            UserRole.Moderator => localizationService.GetString("ROLE_MODER"),
            UserRole.Support => localizationService.GetString("ROLE_SUPPORT"),
            _ => role.ToString()
        };
    }

    public static string GetLocalization(this Action action, ILocalizationService localizationService)
    {
        return action switch
        {
            Action.Registration => localizationService.GetString("ACTION_REGISTRATION"),
            Action.Authorization => localizationService.GetString("ACTION_AUTHORIZATION"),
            Action.BlockingTheUser => localizationService.GetString("ACTION_BLOCKING"),
            Action.UnblockingTheUser => localizationService.GetString("ACTION_UNBLOCKING"),
            Action.PasswordChange => localizationService.GetString("ACTION_PASS_CHANGE"),
            Action.ProfileUpdate => localizationService.GetString("ACTION_PROFILE_UPDATE"),
            Action.GettingUserList => localizationService.GetString("ACTION_GET_USER_LIST"),
            Action.ChangingTheUsersRole => localizationService.GetString("ACTION_CHANGE_ROLE"),
            Action.AddingNewAction => localizationService.GetString("ACTION_ADD_NEW"),
            Action.ActionUpdate => localizationService.GetString("ACTION_UPDATE"),
            _ => action.ToString()
        };
    }

    public static string GetLocalization(this AuthType authType, ILocalizationService localizationService)
    {
        return authType switch
        {
            AuthType.Email => localizationService.GetString("AUTH_TYPE_EMAIL"),
            AuthType.PhoneNumber => localizationService.GetString("AUTH_TYPE_PHONE_NUMBER"),
            AuthType.Vk => localizationService.GetString("AUTH_TYPE_VK"),
            AuthType.Yandex => localizationService.GetString("AUTH_TYPE_YANDEX"),
            _ => authType.ToString()
        };
    }

    public static string GetStyleClass(this Action action)
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

    public static string GetStyleClass(this AuthType authType)
    {
        return authType switch
        {
            AuthType.Email => "positive",
            AuthType.PhoneNumber => "purple",
            AuthType.Vk => "blue",
            AuthType.Yandex => "negative",
            _ => authType.ToString()
        };
    }
}