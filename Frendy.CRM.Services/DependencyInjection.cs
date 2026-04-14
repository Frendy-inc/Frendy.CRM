using Frendy.CRM.Services.Interfaces;
using Frendy.CRM.Services.Services;
using Frendy.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Frendy.CRM.Services;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IClient, DemoClient>();
        services.AddScoped<IJsRuntimeService, JsRuntimeService>();
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<ModalService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<LoaderService>();
    }
}