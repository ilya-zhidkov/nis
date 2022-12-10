using Caliburn.Micro;
using Nis.WpfApp.Requests;

namespace Nis.WpfApp.ViewModels;

public class LoginViewModel : Screen
{
    private string _error;
    private string _userName;
    private string _password;
    private readonly SignInRequest _request;
    private readonly IWindowManager _window;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _aggregator;

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            NotifyOfPropertyChange(() => UserName);
            NotifyOfPropertyChange(() => CanLogin);
        }
    }

    public string Password
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

    public string Error
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

    public LoginViewModel(
        SignInRequest request,
        IWindowManager window,
        SimpleContainer container,
        IEventAggregator aggregator
    )
    {
        _window = window;
        _request = request;
        _container = container;
        _aggregator = aggregator;
    }

    public async Task LoginAsync()
    {
        try
        {
            var response = await _request.SignInAsync(UserName, Password);
            await _window.ShowWindowAsync(new ShellViewModel(_container, response.Student, _aggregator));
            await TryCloseAsync();
        }
        catch (Exception exception)
        {
            Error = exception.Message;
        }
    }
}
