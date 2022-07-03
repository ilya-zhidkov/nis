using Caliburn.Micro;
using Notification.Wpf;
using Nis.WpfApp.Models;
using Nis.WpfApp.Requests;
using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Nis.WpfApp.ViewModels;

public class PatientClassificationViewModel : Screen, IHandle<Assignment>
{
    private Form _form;
    private byte _mistakes;
    private string _anamnesis;
    private Diet _selectedDiet;
    private TimeSpan _timeLeft;
    private Diagnosis _selectedDiagnosis;
    private readonly Countdown _countdown;
    private readonly DataContext _context;
    private Department _selectedDepartment;
    private readonly UploadRequest _request;
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

    public TimeSpan TimeLeft
    {
        get => _timeLeft;
        set
        {
            _timeLeft = value;
            NotifyOfPropertyChange(() => TimeLeft);
            NotifyOfPropertyChange(() => CanProceed);
        }
    }

    public byte Limit => 3;
    public bool CanProceed => SelectedDiet?.Name == "Tekutá" && Mistakes < Limit && TimeLeft > TimeSpan.Zero;
    public bool CanDisplayDiets => SelectedDepartment?.Name == "Kardiologie" && TimeLeft > TimeSpan.Zero;
    public bool CanDisplayDepartments => SelectedDiagnosis?.Name == "Infarkt myokardu" && TimeLeft > TimeSpan.Zero;

    public byte Mistakes
    {
        get => _mistakes;
        set
        {
            _mistakes = value;

            if (_mistakes >= Limit)
                ProceedAsync().ConfigureAwait(false);

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
        UploadRequest request,
        SimpleContainer container,
        IEventAggregator aggregator,
        INotificationManager notifications
    )
    {
        _context = context;
        _request = request;
        _container = container;
        _aggregator = aggregator;
        _notifications = notifications;
        _countdown = new Countdown(TimeSpan.FromMinutes(10));
    }

    public async Task ProceedAsync()
    {
        _countdown.Stop();

        var passed = Mistakes < Limit && TimeLeft > TimeSpan.Zero;

        _form ??= new Form
        {
            Passed = passed,
            Anamnesis = Anamnesis,
            Diet = SelectedDiet.Name,
            Diagnosis = SelectedDiagnosis.Name,
            Department = SelectedDepartment.Name,
            Student = _container.GetInstance<Student>()
        };

        if (passed)
        {
            _container.Instance(_form);
            await _aggregator.PublishOnUIThreadAsync("Activity");
        }
        else
        {
            _notifications.Show("Test přerušen", "Bohužel, kvůli počtu chyb nebo absenci času Vás nemůžeme připustit k vyplnění zdravotních škál.", NotificationType.Warning, "WindowArea");
            await _request.UploadAsync(_form);
        }
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
        _form = _container.GetInstance<Form>();

        TimeLeft = _countdown.Interval;
        StartTimer();

        Diagnoses = new BindableCollection<Diagnosis>((await _context.Diagnoses.ToListAsync())
            .Select(diagnosis => new Diagnosis { Name = diagnosis.Name }));
        Departments = new BindableCollection<Department>((await _context.Departments.ToListAsync())
            .Select(department => new Department { Name = department.Name }));
        Diets = new BindableCollection<Diet>((await _context.Diets.ToListAsync())
            .Select(diet => new Diet { Name = diet.Name }));
    }

    private void StartTimer()
    {
        var time = _countdown.Interval;

        _countdown.Tick += (sender, args) =>
        {
            TimeLeft = time;

            if (time <= TimeSpan.Zero)
                ProceedAsync().ConfigureAwait(false);

            time = time.Subtract(TimeSpan.FromSeconds(1));
        };

        _countdown.Start();
    }
}
