namespace Frendy.CRM.Services.Interfaces;

public interface IJsRuntimeService
{
    Task<T?> GetLocalStorageAsync<T>(string key);
    Task SetLocalStorageAsync(string key, object value);
    Task InvokeVoidAsync(string command);
    Task InvokeVoidAsync(string command, string identifier, params object?[]? args);
    Task<T> InvokeAsync<T>(string command);
    Task<T> InvokeAsync<T>(string command, params object?[]? args);
}