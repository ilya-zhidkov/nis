using Caliburn.Micro;
using Notification.Wpf;
using Nis.WpfApp.Models;
using Nis.Core.Persistence;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;

namespace Nis.WpfApp.ViewModels;

public class PatientClassificationViewModel : Screen, IHandle<Assignment>
{
    private byte _mistakes;
    private double _miutes;
    private double _timeLeft;
    private string _anamnesis;
    private int MMinutes = 10;
    private Diet _selectedDiet;
    private DispatcherTimer _timer;
    private const byte Seconds = 59;
    private Diagnosis _selectedDiagnosis;
    private readonly DataContext _context;
    private Department _selectedDepartment;
    private BindableCollection<Diet> _diets;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _aggregator;
    private BindableCollection<Diagnosis> _diagnoses;
    private BindableCollection<Department> _departments;
    private readonly INotificationManager _notifications;

    public string Anamnesis
    {
        get => _anamnesis;
        set
        {
            _anamnesis = value;
            NotifyOfPropertyChange(() => Anamnesis);
        }
    }

    public double TimeLeft
    {
        get => _timeLeft;
        set
        {
            _timeLeft = value;
            NotifyOfPropertyChange(() => TimeLeft);

            if (_timeLeft <= 0)
            {
                TimeLeft = Seconds;
                Minutes--;
            }
        }
    }

    public double Minutes
    {
        get => _miutes;
        set
        {
            _miutes = value;
            NotifyOfPropertyChange(() => Minutes);
            NotifyOfPropertyChange(() => CanProceed);

            if (_miutes <= 0)
                _notifications.Show("Test nesplněn", "Bohužel, test nebyl splněn v zadaném intervalu.", NotificationType.Warning, "WindowArea");
        }
    }

    public byte Limit => 3;
    public bool CanProceed => SelectedDiet?.Name == "Tekutá" && Mistakes < Limit && TimeLeft > 0;
    public bool CanDisplayDiets => SelectedDepartment?.Name == "Kardiologie" && TimeLeft > 0;
    public bool CanDisplayDepartments => SelectedDiagnosis?.Name == "Infarkt myokardu" && TimeLeft > 0;

    public byte Mistakes
    {
        get => _mistakes;
        set
        {
            _mistakes = value;
            NotifyOfPropertyChange(() => Mistakes);
        }
    }

    public Diagnosis SelectedDiagnosis
    {
        get => _selectedDiagnosis;
        set
        {
            _selectedDiagnosis = value;

            if (_selectedDiagnosis.Name != "Infarkt myokardu")
                Mistakes++;

            NotifyOfPropertyChange(() => SelectedDiagnosis);
            NotifyOfPropertyChange(() => CanDisplayDepartments);
        }
    }

    public Department SelectedDepartment
    {
        get => _selectedDepartment;
        set
        {
            _selectedDepartment = value;

            if (_selectedDepartment.Name != "Kardiologie")
                Mistakes++;

            NotifyOfPropertyChange(() => SelectedDepartment);
            NotifyOfPropertyChange(() => CanDisplayDiets);
        }
    }

    public Diet SelectedDiet
    {
        get => _selectedDiet;
        set
        {
            _selectedDiet = value;

            if (_selectedDiet.Name != "Tekutá")
                Mistakes++;

            NotifyOfPropertyChange(() => SelectedDiet);
            NotifyOfPropertyChange(() => CanProceed);
        }
    }

    public BindableCollection<Diagnosis> Diagnoses
    {
        get => _diagnoses;
        set
        {
            _diagnoses = value;
            NotifyOfPropertyChange(() => Diagnoses);
        }
    }

    public BindableCollection<Department> Departments
    {
        get => _departments;
        set
        {
            _departments = value;
            NotifyOfPropertyChange(() => Departments);
        }
    }

    public BindableCollection<Diet> Diets
    {
        get => _diets;
        set
        {
            _diets = value;
            NotifyOfPropertyChange(() => Diets);
        }
    }

    public PatientClassificationViewModel(
        DataContext context,
        SimpleContainer container,
        IEventAggregator aggregator,
        INotificationManager notifications
    )
    {
        _context = context;
        _container = container;
        _aggregator = aggregator;
        _notifications = notifications;
    }

    public async Task ProceedAsync()
    {
        _timer.Stop();

        _container.Instance(new Form
        {
            Anamnesis = Anamnesis,
            Diet = SelectedDiet.Name,
            Diagnosis = SelectedDiagnosis.Name,
            Department = SelectedDepartment.Name,
            Student = _container.GetInstance<Student>()
        });

        await _aggregator.PublishOnUIThreadAsync("Activity");
    }

    public async Task HandleAsync(Assignment message, CancellationToken cancellationToken) => await Task.FromResult(Anamnesis = message.Intro);

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _aggregator.SubscribeOnPublishedThread(this);

        return base.OnActivateAsync(cancellationToken);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        _aggregator.Unsubscribe(this);

        return base.OnDeactivateAsync(close, cancellationToken);
    }

    protected override async void OnViewLoaded(object view)
    {
        Diagnoses = new BindableCollection<Diagnosis>((await _context.Diagnoses.ToListAsync())
            .Select(diagnosis => new Diagnosis { Name = diagnosis.Name }));
        Departments = new BindableCollection<Department>((await _context.Departments.ToListAsync())
            .Select(department => new Department { Name = department.Name }));
        Diets = new BindableCollection<Diet>((await _context.Diets.ToListAsync())
            .Select(diet => new Diet { Name = diet.Name }));

        StartTimer();
    }

    private void StartTimer()
    {
        Minutes = MMinutes;
        TimeLeft = 0;

        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
        _timer.Tick += timer_Tick;
        _timer.Start();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        TimeLeft--;

        if (Minutes == 0 || Mistakes >= Limit)
            _timer.Stop();
    }
}
