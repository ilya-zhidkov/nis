using Caliburn.Micro;
using System.Windows;
using Notification.Wpf;
using Nis.Core.Persistence;
using Nis.WpfApp.Extensions;
using Nis.WpfApp.ViewModels;
using Nis.WpfApp.Conventions;
using System.Windows.Controls;
using Nis.Core.Persistence.Seeders;

namespace Nis.WpfApp;

public class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new();

    public Bootstrapper()
    {
        Initialize();

        ConventionManager.AddElementConvention<PasswordBox>(
            bindableProperty: PasswordBoxConvention.BoundPasswordProperty,
            parameterProperty: "Password",
            eventName: "PasswordChanged"
        );
    }

    protected override void Configure()
    {
        _container.Instance(_container);

        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .Singleton<INotificationManager, NotificationManager>()
            .RegisterRequests()
            .RegisterViewModels()
            .RegisterMappings()
            .RegisterDatabase();

        SeedDatabase();
    }

    protected override void OnStartup(object sender, StartupEventArgs e) => DisplayRootViewFor<LoginViewModel>();

    protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);

    protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);

    protected override void BuildUp(object instance) => _container.BuildUp(instance);

    private void SeedDatabase()
    {
        var context = _container.GetInstance<DataContext>();
        context.Database.EnsureCreated();
        new DepartmentSeeder().Seed(context);
        new DiagnosisSeeder().Seed(context);
        new DietSeeder().Seed(context);
        new ExamSeeder().Seed(context);
        new MedicalScalesSeeder().Seed(context);
        context.SaveChanges();
    }
}
