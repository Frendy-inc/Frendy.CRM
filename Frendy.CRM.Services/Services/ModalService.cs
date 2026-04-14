using Frendy.CRM.Shared.Models;

namespace Frendy.CRM.Services.Services;

public class ModalService
{
    public event Action<ModalInstance>? OnOpen;
    public event Action? OnClose;
    
    private readonly Stack<TaskCompletionSource<object?>> _stack = new();

    public Task<T?> OpenAsync<TComponent, T>(Dictionary<string, object>? parameters = null)
    {
        var tcs = new TaskCompletionSource<object?>();
        _stack.Push(tcs);

        OnOpen?.Invoke(new ModalInstance(
            typeof(TComponent),
            parameters ?? new Dictionary<string, object>()
        ));

        return tcs.Task.ContinueWith(t => (T?)t.Result);
    }

    public void Close(object? result = null)
    {
        if (_stack.TryPop(out var tcs))
        {
            tcs.TrySetResult(result);
        }

        OnClose?.Invoke();
    }
}