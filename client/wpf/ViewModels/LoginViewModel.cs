using Nis.WpfApp.Requests;

namespace Nis.WpfApp.ViewModels;

[UsedImplicitly]
public class LoginViewModel(
    SignInRequest request,
    IWindowManager window,
    SimpleContainer container,
    IEventAggregator aggregator
) : Screen
{
    private string? _error;
    private string? _userName;
    private string? _password;

    public string? UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            NotifyOfPropertyChange(() => UserName);
            NotifyOfPropertyChange(() => CanLogin);
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            NotifyOfPropertyChange(() => Password);
            NotifyOfPropertyChange(() => CanLogin);
        }
    }

    public bool CanLogin => UserName?.Length > 0 && Password?.Length > 0;

    public string? Error
    {
        get => _error;
        set
        {
            _error = value;
            NotifyOfPropertyChange(() => HasErrors);
            NotifyOfPropertyChange(() => Error);
        }
    }

    public bool HasErrors => Error?.Length > 0;

    public async Task LoginAsync()
    {
        try
        {
            var response = await request.SignInAsync(UserName, Password);
            await window.ShowWindowAsync(new ShellViewModel(container, response?.Student, aggregator));
            await TryCloseAsync();
        }
        catch (Exception exception)
        {
            Error = exception.Message;
        }
    }
}
