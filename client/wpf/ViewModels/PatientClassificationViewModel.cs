using Notification.Wpf;
using Nis.WpfApp.Models;
using Nis.WpfApp.Requests;
using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Nis.WpfApp.ViewModels;

[UsedImplicitly]
internal class PatientClassificationViewModel(
    DataContext context,
    UploadRequest request,
    SimpleContainer container,
    IEventAggregator aggregator,
    INotificationManager notifications
) : Screen, IHandle<Assignment>
{
    private Form? _form;
    private byte _mistakes;
    private TimeSpan _timeLeft;
    private string? _anamnesis;
    private Diet? _selectedDiet;
    private BindableCollection<Diet> _diets = [];
    private Diagnosis? _selectedDiagnosis;
    private Department? _selectedDepartment;
    private BindableCollection<Diagnosis> _diagnoses = [];
    private BindableCollection<Department> _departments = [];
    private readonly Countdown _countdown = new(TimeSpan.FromMinutes(10));

    public string? Anamnesis
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

    public static byte Limit => 3;
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

    public Diagnosis? SelectedDiagnosis
    {
        get => _selectedDiagnosis;
        set
        {
            _selectedDiagnosis = value;

            if (_selectedDiagnosis?.Name != "Infarkt myokardu")
                Mistakes++;

            NotifyOfPropertyChange(() => SelectedDiagnosis);
            NotifyOfPropertyChange(() => CanDisplayDepartments);
        }
    }

    public Department? SelectedDepartment
    {
        get => _selectedDepartment;
        set
        {
            _selectedDepartment = value;

            if (_selectedDepartment?.Name != "Kardiologie")
                Mistakes++;

            NotifyOfPropertyChange(() => SelectedDepartment);
            NotifyOfPropertyChange(() => CanDisplayDiets);
        }
    }

    public Diet? SelectedDiet
    {
        get => _selectedDiet;
        set
        {
            _selectedDiet = value;

            if (_selectedDiet?.Name != "Tekutá")
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

    public async Task ProceedAsync()
    {
        _countdown.Stop();

        var passed = Mistakes < Limit && TimeLeft > TimeSpan.Zero;

        _form ??= new()
        {
            Passed = passed,
            Anamnesis = Anamnesis,
            Diet = SelectedDiet?.Name!,
            Diagnosis = SelectedDiagnosis?.Name!,
            Department = SelectedDepartment?.Name!,
            Student = container.GetInstance<Student>()
        };

        if (passed)
        {
            container.Instance(_form);
            await aggregator.PublishOnUIThreadAsync("Activity");
        }
        else
        {
            notifications.Show("Test přerušen", "Bohužel, kvůli počtu chyb nebo absenci času Vás nemůžeme připustit k vyplnění zdravotních škál.", NotificationType.Warning, "WindowArea");
            await request.UploadAsync(_form);
        }
    }

    public async Task HandleAsync(Assignment message, CancellationToken cancellation) => await Task.FromResult(Anamnesis = message.Intro);

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        aggregator.SubscribeOnPublishedThread(this);

        return base.OnActivateAsync(cancellationToken);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellation)
    {
        aggregator.Unsubscribe(this);

        return base.OnDeactivateAsync(close, cancellation);
    }

    protected override async void OnViewLoaded(object view)
    {
        _form = container.GetInstance<Form>();

        TimeLeft = _countdown.Interval;
        StartTimer();

        Diagnoses = new((await context.Diagnoses.ToListAsync()).Select(diagnosis => new Diagnosis { Name = diagnosis.Name }));
        Departments = new((await context.Departments.ToListAsync()).Select(department => new Department { Name = department.Name }));
        Diets = new((await context.Diets.ToListAsync()).Select(diet => new Diet { Name = diet.Name }));
    }

    private void StartTimer()
    {
        var time = _countdown.Interval;

        _countdown.Tick += (_, _) =>
        {
            TimeLeft = time;

            if (time <= TimeSpan.Zero)
                ProceedAsync().ConfigureAwait(false);

            time = time.Subtract(TimeSpan.FromSeconds(1));
        };

        _countdown.Start();
    }
}
