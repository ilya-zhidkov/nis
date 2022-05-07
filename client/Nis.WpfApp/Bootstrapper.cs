using Caliburn.Micro;
using System.Windows;
using Nis.Core.Persistence;
using Nis.WpfApp.Extensions;
using Nis.WpfApp.ViewModels;
using Nis.Core.Persistence.Seeders;

namespace Nis.WpfApp;

public class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new();

    public Bootstrapper() => Initialize();

    protected override void Configure()
    {
        _container.Instance(_container);

        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .RegisterViewModels()
            .RegisterMappings()
            .RegisterDatabase();

        SeedDatabase();
    }

    protected override void OnStartup(object sender, StartupEventArgs e) => DisplayRootViewFor<ShellViewModel>();

    protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);

    protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);

    protected override void BuildUp(object instance) => _container.BuildUp(instance);

    private void SeedDatabase()
    {
        var context = _container.GetInstance<DataContext>();
        context.Database.EnsureCreated();
        new PatientSeeder().Seed(context);
        context.SaveChanges();
    }
}
