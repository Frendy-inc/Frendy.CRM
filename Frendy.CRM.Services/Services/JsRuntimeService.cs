using System.Text.Json;
using Frendy.CRM.Services.Interfaces;
using Microsoft.JSInterop;

namespace Frendy.CRM.Services.Services;

public class JsRuntimeService : IJsRuntimeService
{
    private readonly IJSRuntime _jsRuntime;

    public JsRuntimeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetLocalStorageAsync<T>(string key)
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (string.IsNullOrEmpty(json))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            });

        }
        catch (Exception)
        {
            return default;
        }

    }

    public async Task SetLocalStorageAsync(string key, object value) =>
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));

    public async Task InvokeVoidAsync(string command) =>
        await _jsRuntime.InvokeVoidAsync(command);

    public async Task InvokeVoidAsync(string command, string identifier, params object?[]? args) =>
        await _jsRuntime.InvokeVoidAsync(command, identifier, args);

    public async Task<T> InvokeAsync<T>(string command) =>
        await _jsRuntime.InvokeAsync<T>(command);
    
    public async Task<T> InvokeAsync<T>(string command, params object?[]? args) =>
        await _jsRuntime.InvokeAsync<T>(command, args);
}