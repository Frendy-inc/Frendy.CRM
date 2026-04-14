using Frendy.CRM.Services.Interfaces;
using Frendy.CRM.Shared.Models;
using Frendy.Shared.Dto.RequestDto.AuditRequestDto;
using Frendy.Shared.Dto.RequestDto.StaffRequestDto;
using Frendy.Shared.Dto.RequestDto.UserRequestDto;
using Frendy.Shared.Enums;
using Frendy.Shared.Extensions;
using Action = Frendy.Shared.Enums.Action;

namespace Frendy.CRM.Services.Services;

public class DemoClient : IClient
{
    private readonly IJsRuntimeService _jsRuntime;
    
    private readonly string[] _firstNames = ["Иван", "Пётр", "Сергей", "Олег", "Николай", "Алексей", "Дмитрий", "Евгений", "Кирилл", "Максим"];
    private readonly string[] _lastNames = ["Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Васильев", "Новиков", "Морозов", "Ершов"];
    private readonly UserRole[] _roles = [UserRole.User, UserRole.Moderator, UserRole.Administrator];
    private readonly string[] _cities = ["Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань", "Нижний Новгород", "Челябинск"];
    private readonly string[] _deviceNames = ["DESKTOP-PC01", "LAPTOP-USER42", "WORKSTATION-03", "MOBILE-DEVICE", "SERVER-APP01"];
    private readonly string[] _domains = ["example.com", "company.ru", "mail.com", "test.org"];
    
    public DemoClient(IJsRuntimeService jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    private List<AuditShortDetailsLookup> AuditShortDetails { get; set; } = [];
    private List<UserShortDetailsLookup> UserShortDetails { get; set; } = [];
    private List<StaffShortDetailsLookup> StaffShortDetails { get; set; } = [];

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
                Role = UserRole.Administrator,
                Avatar = "https://udoba.org/sites/default/files/h5p/content/253631/images/collageClip-69393d5b415b6.jpg"
            };
            await _jsRuntime.SetLocalStorageAsync("currentUser", currentUser);
        }
        
        return currentUser;
    }

    public async Task<AuditShortDetails> GetAuditsAsync(GetAuditsRequestDto requestDto)
    {            
        await Task.Delay(1000);
        var details = new List<AuditShortDetailsLookup>();
        
        var actions = new[] { Action.PasswordChange, Action.BlockingTheUser, Action.Authorization, Action.Registration, Action.UnblockingTheUser, Action.ActionUpdate, Action.AddingNewAction, Action.GettingUserList };

        var now = DateTime.Now;

        if (AuditShortDetails.Count == 0)
        {
            for (var i = details.Count; i < 2500; i++)
            {
                var execFirst = _firstNames[i % _firstNames.Length];
                var execLast = _lastNames[i % _lastNames.Length];
                var targetFirst = _firstNames[(i + 3) % _firstNames.Length];
                var targetLast = _lastNames[(i + 5) % _lastNames.Length];

                details.Add(new AuditShortDetailsLookup
                {
                    Id = Guid.NewGuid(),
                    Action = actions[i % actions.Length],
                    ActionDate = now.AddMinutes(-i),
                    ExecutorId = Guid.NewGuid(),
                    ExecutorFirstName = execFirst,
                    ExecutorLastName = execLast,
                    ExecutorRole = _roles[i % _roles.Length],
                    TargetId = Guid.NewGuid(),
                    TargetFirstName = targetFirst,
                    TargetLastName = targetLast,
                    TargetRole = _roles[(i + 1) % _roles.Length]
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
            PageSize = 20
        };

        return audits;
    }

    public async Task<AuditDetails?> GetAuditDetailsAsync(Guid auditId)
    {        
        await Task.Delay(1000);
        var random = new Random();
    
        if (AuditShortDetails.Count == 0) return null;
        
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
            ExecutorDeviceName = _deviceNames[random.Next(_deviceNames.Length)],
            ExecutorCity = _cities[random.Next(_cities.Length)],
            ExecutorEmail = GenerateEmail(action.ExecutorFirstName, action.ExecutorLastName),
            ActionDescription = random.Next(2) == 0 ? actionDescriptions[random.Next(actionDescriptions.Length)] : null,
            ExecutorPhoneNumber = GeneratePhoneNumber(),
            ExecutorIpAddress = GenerateIpAddress(),
            ExecutorUsername = GenerateUsername(action.ExecutorFirstName, action.ExecutorLastName),
            TargetPhoneNumber = GeneratePhoneNumber(),
            TargetUsername = GenerateUsername(action.TargetFirstName, action.TargetLastName),
            TargetEmail = GenerateEmail(action.TargetFirstName, action.TargetLastName)
        };
    }

    public async Task<UserShortDetails> GetUsersAsync(GetUsersRequestDto requestDto)
    {
        var random = new Random();
        var details = new List<UserShortDetailsLookup>();

        var authTypes = new[] { AuthType.Email, AuthType.PhoneNumber, AuthType.Vk, AuthType.Yandex };
        var now = DateTime.Now;

        if (UserShortDetails.Count == 0)
        {
            for (var i = details.Count; i < 2500; i++)
            {
                var firstName = _firstNames[i % _firstNames.Length];
                var lastName = _lastNames[i % _lastNames.Length];

                details.Add(new UserShortDetailsLookup
                {
                    Id = Guid.NewGuid(),
                    FirstName = firstName,
                    LastName = lastName,
                    Role = _roles[i % _roles.Length],
                    Email = GenerateEmail(firstName, lastName),
                    PhoneNumber = GeneratePhoneNumber(),
                    LastActivity = now.AddMinutes(-i),
                    IsBanned = random.Next(2) == 0,
                    IsOnline = random.Next(2) == 0,
                    AuthType = authTypes[i % authTypes.Length],
                });
            }
            
            UserShortDetails = details;
        }
        else
        {
            details = UserShortDetails;
        }
        
        if (!requestDto.Search.IsNullOrEmpty())
        {
            details = UserShortDetails.Where(x => 
                x.FirstName.Contains(requestDto.Search!) || 
                x.LastName.Contains(requestDto.Search!) || 
                x.PhoneNumber.Contains(requestDto.Search!) || 
                x.Email.Contains(requestDto.Search!)).ToList();
        }
        
        var audits = new UserShortDetails
        {
            Details = details.Skip(requestDto.From).Take(requestDto.PageSize).ToList(),
            Page =  requestDto.From / requestDto.PageSize + 1,
            TotalCount = details.Count,
            PageSize = 20
        };
        
        return audits;
    }

    public async Task<UserDetails?> GetUserDetailsAsync(Guid userId)
    {        
        var random = new Random();
        var date = DateTime.Now;
        
        if (UserShortDetails.Count == 0) return null;
        
        var user = UserShortDetails.FirstOrDefault(x => x.Id == userId);
        if (user is null) return null;
        

        return new UserDetails
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            LastActivity = user.LastActivity,
            IsBanned = user.IsBanned,
            IsOnline = user.IsOnline,
            AuthType = user.AuthType,
            Role = user.Role,
            Birthday = date.AddYears(random.Next(-30, -16)),
            CreatedAt = date.AddMonths(random.Next(-28, 0)),
            DeviceName = _deviceNames[random.Next(_deviceNames.Length)],
            City = _cities[random.Next(_cities.Length)],
            Avatar = random.Next(2) == 0 ? "https://udoba.org/sites/default/files/h5p/content/253631/images/collageClip-69393d5b415b6.jpg" : null,
            Username = GenerateUsername(user.FirstName, user.LastName),
            IpAddress = GenerateIpAddress()
        };
    }

    public async Task<StaffShortDetails> GetStaffsAsync(GetStaffsRequestDto requestDto)
    {
        var random = new Random();
        var details = new List<StaffShortDetailsLookup>();
        
        var now = DateTime.Now;

        if (StaffShortDetails.Count == 0)
        {
            for (var i = details.Count; i < 2500; i++)
            {
                var firstName = _firstNames[i % _firstNames.Length];
                var lastName = _lastNames[i % _lastNames.Length];

                details.Add(new StaffShortDetailsLookup
                {
                    Id = Guid.NewGuid(),
                    FirstName = firstName,
                    LastName = lastName,
                    Role = _roles[i % _roles.Length],
                    Email = GenerateEmail(firstName, lastName),
                    PhoneNumber = GeneratePhoneNumber(),
                    LastActivity = now.AddMinutes(-i),
                    IsOnline = random.Next(2) == 0,
                    AssignerFirstName = _firstNames[i % _firstNames.Length],
                    AssignerLastName = _lastNames[i % _lastNames.Length],
                    AssignerRole = _roles[i % _roles.Length],
                    AssignDate = now.AddDays(random.Next(-30, -16))
                });
            }
            
            StaffShortDetails = details;
        }
        else
        {
            details = StaffShortDetails;
        }
        
        if (!requestDto.Search.IsNullOrEmpty())
        {
            details = StaffShortDetails.Where(x => 
                x.FirstName.Contains(requestDto.Search!) || 
                x.LastName.Contains(requestDto.Search!) || 
                x.PhoneNumber.Contains(requestDto.Search!) || 
                x.Email.Contains(requestDto.Search!)).ToList();
        }
        
        var audits = new StaffShortDetails
        {
            Details = details.Skip(requestDto.From).Take(requestDto.PageSize).ToList(),
            Page =  requestDto.From / requestDto.PageSize + 1,
            TotalCount = details.Count,
            PageSize = 20
        };
        
        return audits;
    }

    public async Task<StaffDetails?> GetStaffDetailsAsync(Guid staffId)
    {        
        var random = new Random();
        
        if (StaffShortDetails.Count == 0) return null;
        
        var staff = StaffShortDetails.FirstOrDefault(x => x.Id == staffId);
        if (staff is null) return null;
        

        return new StaffDetails
        {
            Id = staff.Id,
            FirstName = staff.FirstName,
            LastName = staff.LastName,
            Email = staff.Email,
            PhoneNumber = staff.PhoneNumber,
            LastActivity = staff.LastActivity,
            IsBanned = random.Next(2) == 0,
            IsOnline = staff.IsOnline,
            Role = staff.Role,
            Avatar = random.Next(2) == 0 ? "https://udoba.org/sites/default/files/h5p/content/253631/images/collageClip-69393d5b415b6.jpg" : null,
            Username = GenerateUsername(staff.FirstName, staff.LastName),
            DeviceName = _deviceNames[random.Next(_deviceNames.Length)],
            AssignDate = staff.AssignDate,
            AssignerFirstName = staff.AssignerFirstName,
            AssignerLastName = staff.AssignerLastName,
            AssignerRole = staff.AssignerRole,
            AssignerEmail = GenerateEmail(staff.AssignerFirstName, staff.AssignerLastName),
            AssignerUsername = GenerateUsername(staff.AssignerFirstName, staff.AssignerLastName),
            AssignerPhoneNumber = GeneratePhoneNumber()
        };
    }

    public async Task DemoteStaffAsync(Guid staffId)
    
    {
        if (StaffShortDetails.Count == 0) return;
        
        var staff = StaffShortDetails.FirstOrDefault(x => x.Id == staffId);
        if (staff is null) return;
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

    private string? GenerateEmail(string firstName, string lastName)
    {
        var random = new Random();
        return random.Next(2) == 0
            ? $"{Transliterate(firstName)}.{Transliterate(lastName)}@{_domains[random.Next(_domains.Length)]}".ToLower()
            : null;
    }

    private static string? GenerateUsername(string firstName, string lastName)
    {
        var random = new Random();
        return random.Next(2) == 0 ? $"{Transliterate(firstName)}.{Transliterate(lastName)}".ToLower() : null;
    }

    private static string? GeneratePhoneNumber()
    {
        var random = new Random();
        return random.Next(2) == 0
            ? $"7{random.Next(900, 1000)}{random.Next(100, 1000)}{random.Next(10, 100)}{random.Next(10, 100)}"
            : null;
    }

    private static string GenerateIpAddress()
    {
        var random = new Random();
        return $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(1, 255)}";
    }
}