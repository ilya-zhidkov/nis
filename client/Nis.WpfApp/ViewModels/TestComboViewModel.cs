using AutoMapper;
using Caliburn.Micro;
using Nis.Core.Persistence;

namespace Nis.WpfApp.ViewModels;

public class TestComboViewModel : Screen
{
    private readonly SimpleContainer _container;
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;

    public TestComboViewModel(IWindowManager window, SimpleContainer container, DataContext context, IMapper mapper)
    {
        _window = window;
        _container = container;
        _context = context;
        _mapper = mapper;
    }


    public void ShowComboBox()
    {
        _window.ShowWindowAsync(new TestCheckViewModel());
    }
}
