namespace Frendy.CRM.Services.Services;

public class LoaderService
{
    public event Action? OnShow;
    public event Action? OnHide;

    private int _counter = 0;

    public void Show()
    {
        _counter++;
        OnShow?.Invoke();
    }

    public void Hide()
    {
        if (_counter <= 0) return;

        _counter--;

        if (_counter == 0)
            OnHide?.Invoke();
    }
    
    public async Task RunAsync(Func<Task> action)
    {
        Show();

        try
        {
            await action();
        }
        finally
        {
            Hide();
        }
    }
    
    public async Task<T> RunAsync<T>(Func<Task<T>> action)
    {
        Show();

        try
        {
            return await action();
        }
        finally
        {
            Hide();
        }
    }
}