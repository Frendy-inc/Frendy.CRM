using System.Diagnostics;
using Frendy.CRM.Services.Interfaces;
using Frendy.CRM.Shared;
using Frendy.CRM.Shared.Models;
using Frendy.Shared.Dto.RequestDto.AuditRequestDto;
using Frendy.Shared.Enums;
using Frendy.Shared.Extensions;
using Action = Frendy.Shared.Enums.Action;

namespace Frendy.CRM.Services.Services;

public class DemoClient : IClient
{
    private readonly IJsRuntimeService _jsRuntime;

    public DemoClient(IJsRuntimeService jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    private List<AuditShortDetailsLookup> AuditShortDetails { get; set; } = [];

    public async Task<CurrentUserDetails> GetCurrentUserDetailsAsync()
    {
        var currentUser = await _jsRuntime.GetLocalStorageAsync<CurrentUserDetails>("currentUser");
        
        if (currentUser is null)
        {
            currentUser = new CurrentUserDetails
            {
                FirstName = "Аркадий",
                LastName = "Алфёров",
                FullName = "Алфёров Аркадий",
                Email = "arhe414@gmail.com",
                UserName = "mipesync",
                Role = UserRole.Administrator
            };
            await _jsRuntime.SetLocalStorageAsync("currentUser", currentUser);
        }
        
        return currentUser;
    }

    public async Task<AuditShortDetails> GetAuditsAsync(GetAuditsRequestDto requestDto)
    {
        AppState.ChangeLoadingState(true);

        var details = new List<AuditShortDetailsLookup>();
        
        var firstNames = new[] { "Иван", "Пётр", "Сергей", "Олег", "Николай", "Алексей", "Дмитрий", "Евгений", "Кирилл", "Максим" };
        var lastNames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Васильев", "Новиков", "Морозов", "Ершов" };
        var roles = new[] { UserRole.User, UserRole.Moderator, UserRole.Administrator };
        var actions = new[] { Action.PasswordChange, Action.BlockingTheUser, Action.Authorization, Action.Registration, Action.UnblockingTheUser, Action.ActionUpdate, Action.AddingNewAction, Action.GettingUserList };

        var now = DateTime.Now;

        if (AuditShortDetails.Count == 0)
        {
            for (var i = details.Count; i < 2500; i++)
            {
                var execFirst = firstNames[i % firstNames.Length];
                var execLast = lastNames[i % lastNames.Length];
                var targetFirst = firstNames[(i + 3) % firstNames.Length];
                var targetLast = lastNames[(i + 5) % lastNames.Length];

                details.Add(new AuditShortDetailsLookup
                {
                    Id = Guid.NewGuid(),
                    Action = actions[i % actions.Length],
                    ActionDate = now.AddMinutes(-i),
                    ExecutorId = Guid.NewGuid(),
                    ExecutorFirstName = execFirst,
                    ExecutorLastName = execLast,
                    ExecutorRole = roles[i % roles.Length],
                    TargetId = Guid.NewGuid(),
                    TargetFirstName = targetFirst,
                    TargetLastName = targetLast,
                    TargetRole = roles[(i + 1) % roles.Length]
                });
            }
            
            AuditShortDetails = details;
        }
        else
        {
            details = AuditShortDetails;
        }
        
        if (!requestDto.Search.IsNullOrEmpty())
        {
            details = AuditShortDetails.Where(x => 
                x.ExecutorFirstName.Contains(requestDto.Search!) || 
                x.ExecutorLastName.Contains(requestDto.Search!) || 
                x.TargetFirstName.Contains(requestDto.Search!) || 
                x.TargetLastName.Contains(requestDto.Search!) ||
                x.Action.ToString().Contains(requestDto.Search!)).ToList();
        }
        
        var audits = new AuditShortDetails
        {
            Details = details.Skip(requestDto.From).Take(requestDto.PageSize).ToList(),
            Page =  requestDto.From / requestDto.PageSize + 1,
            TotalCount = details.Count,
            PageSize = 10
        };
        
        AppState.ChangeLoadingState(false);
        return audits;
    }

    public async Task<AuditDetails?> GetAuditDetailsAsync(Guid auditId)
    {
        var random = new Random();
    
        if (AuditShortDetails.Count == 0) return null;
        
        var cities = new[] { "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань", "Нижний Новгород", "Челябинск" };
        var deviceNames = new[] { "DESKTOP-PC01", "LAPTOP-USER42", "WORKSTATION-03", "MOBILE-DEVICE", "SERVER-APP01" };
        var domains = new[] { "example.com", "company.ru", "mail.com", "test.org" };
        var actionDescriptions = new[] 
        { 
            "Обновлен профиль пользователя", 
            "Изменен пароль", 
            "Изменены права доступа", 
            "Создана учетная запись",
            "Экспортированы данные",
            "Удален файл",
            "Добавлена запись"
        };
        
        var action = AuditShortDetails.FirstOrDefault(x => x.Id == auditId);
        if (action is null) return null;
        
        return new AuditDetails
        {
            Id = action.Id,
            ExecutorId = action.ExecutorId,
            ExecutorFirstName = action.ExecutorFirstName,
            ExecutorLastName = action.ExecutorLastName,
            ExecutorRole = action.ExecutorRole,
            TargetId = action.TargetId,
            TargetFirstName = action.TargetFirstName,
            TargetLastName = action.TargetLastName,
            TargetRole = action.TargetRole,
            ActionDate = action.ActionDate,
            Action = action.Action,
            DeviceName = deviceNames[random.Next(deviceNames.Length)],
            City = cities[random.Next(cities.Length)],
            Email = random.Next(2) == 0 ? $"{Transliterate(action.ExecutorFirstName)}.{Transliterate(action.ExecutorLastName)}@{domains[random.Next(domains.Length)]}".ToLower() : null,
            ActionDescription = random.Next(2) == 0 ? actionDescriptions[random.Next(actionDescriptions.Length)] : null
        };
    }
    
    private static string Transliterate(string text)
    {
        var map = new Dictionary<char, string>
        {
            {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "yo"},
            {'ж', "zh"}, {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"},
            {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"},
            {'ф', "f"}, {'х', "h"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "sch"}, {'ъ', ""},
            {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
        };
    
        var result = "";
        foreach (var c in text.ToLower())
        {
            result += map.ContainsKey(c) ? map[c] : c.ToString();
        }
        return result;
    }
}