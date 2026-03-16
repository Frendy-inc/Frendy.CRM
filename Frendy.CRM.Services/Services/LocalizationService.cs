using System.Globalization;
using System.Reflection;
using System.Resources;
using Frendy.Shared.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Frendy.CRM.Services.Services;

public class LocalizationService : ILocalizationService
{
    private readonly object[] _defaultArgs = [];
    private readonly IStringLocalizer _localizer;
    private readonly ResourceManager _resourceManager;
    
    private ILogger<LocalizationService> Logger { get; }
    public event EventHandler<EventArgs>? LocalizationOptionsChanged;
    public event EventHandler<EventArgs>? LanguageChanged;
    
    public LocalizationService(ILogger<LocalizationService> logger, IStringLocalizer localizer)
    {
        Logger = logger;
        _localizer = localizer;
        _resourceManager = GetResourceManager();
    }

    public string GetString(string key)
    {
        return GetString(key, _defaultArgs);
    }

    public virtual string GetString(string key, params object[] arguments)
    {
        return _localizer.GetString(key, arguments);
    }

    public string GetStringUpper(string key)
    {
        return GetStringUpper(key, _defaultArgs);
    }

    public string GetStringUpper(string key, params object[] arguments)
    {
        return GetString(key, arguments).ToUpper();
    }

    public string GetStringLower(string key)
    {
        return GetStringLower(key, _defaultArgs);
    }

    public string GetStringLower(string key, params object[] arguments)
    {
        return GetString(key, arguments).ToLower();
    }

    private ResourceManager GetResourceManager()
    {
        return new ResourceManager("Frendy.CRM.Resources.Properties.ResourcesClass", 
            Assembly.Load("Frendy.CRM.Resources"));
    }

    public virtual ValueTask<IEnumerable<CultureInfo>> GetSupportedCulturesAsync(CancellationToken cToken = default)
    {
        var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var supportedCultures = cultures
            .Where(culture => !string.IsNullOrEmpty(culture.Name) && culture.Name.Length > 2)
            .Where(culture =>
            {
                try
                {
                    var resourceSet = _resourceManager.GetResourceSet(culture, true, false);
                    return resourceSet != null;
                }
                catch (CultureNotFoundException ex)
                {
                    Logger.LogError(ex, $"Could not obtain resource set for {culture}");
                    return false;
                }
            })
            .DistinctBy(x => x.LCID)
            .Select(culture => new CultureInfo(culture.LCID))
            .ToList();

        if (supportedCultures.Count == 0)
            supportedCultures.Add(new CultureInfo("en-US"));

        return new ValueTask<IEnumerable<CultureInfo>>(supportedCultures);
    }

    public Task SetCurrentCultureAsync(CultureInfo culture)
    {
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        
        LanguageChanged?.Invoke(this, EventArgs.Empty);
        return Task.CompletedTask;
    }
}