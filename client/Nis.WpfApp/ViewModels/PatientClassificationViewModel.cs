using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.WpfApp.Messages;
using Nis.WpfApp.Requests;
using Nis.Core.Persistence;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;
using Notification.Wpf;

namespace Nis.WpfApp.ViewModels;

public class PatientClassificationViewModel : Screen
{
    private readonly DataContext _context;
    private readonly UploadRequest _request;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _eventAggregator;
    private readonly INotificationManager _notificationManager;
    private byte _attempts;
    private double _timeLeft;
    private double _miutes;
    private Diet _selectedDiet;
    private const byte _limit = 3;
    private DispatcherTimer _timer;
    private const byte Seconds = 59;
    private int MMinutes = 10;
    private string _anamnesis = @"Na oddělení JIP byl přivezen RZS 56letý pacient. V anamnéze uvedl náhlé svíravé bolesti za hrudní kostí s vystřelováním do levé končetiny a do krku, které neustávaly. Měl pocit úzkosti a strach ze smrti. Uváděl i dušnost. Tento stav začal v zaměstnání po dlouhém a konfliktním jednání. Pracuje jako soukromý podnikatel. Nikdy žádné potíže neměl, ale od 45let se léčí na vysoký TK pomocí farmakologické léčby. Pravidelně chodí na kontroly, nedodržuje žádnou dietu ani správnou životosprávu. Pravidelné cvičení ani sport neprovozuje. Občas jezdí na kole a hraje tenis. Hodně pracuje, až 12-14hod. denně. RZS stabilizovala stav podáním léků do periferní žilní kanyly, sledovala EKG a FF do příjezdu, podávala nemocnému kyslík. Po základním vyšetření a stabilizování stavu bylo rozhodnuto provést u nemocného PTCA(PCI).";
    private Diagnosis _selectedDiagnosis;
    private Department _selectedDepartment;
    private BindableCollection<Diet> _diets;
    private BindableCollection<Diagnosis> _diagnoses;
    private BindableCollection<Department> _departments;

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
            NotifyOfPropertyChange(() => CanSubmit);
            if (_miutes <= 0)
            {
                _notificationManager.Show("Test nesplněn", "Bohužel, test nebyl splněn v zadaném intervalu.",
                    NotificationType.Warning, "WindowArea");
            }
        }
    }

    public byte Limit => _limit;
    public bool CanSubmit => SelectedDiet?.Name == "Tekutá" && Attempts < Limit && TimeLeft > 0;
    public bool CanDisplayDiets => SelectedDepartment?.Name == "Kardiologie" && TimeLeft > 0;
    public bool CanDisplayDepartments => SelectedDiagnosis?.Name == "Infarkt myokardu" && TimeLeft > 0;

    public byte Attempts
    {
        get => _attempts;
        set
        {
            _attempts = value;
            NotifyOfPropertyChange(() => Attempts);
        }
    }

    public Diagnosis SelectedDiagnosis
    {
        get => _selectedDiagnosis;
        set
        {
            _selectedDiagnosis = value;

            if (_selectedDiagnosis.Name != "Infarkt myokardu")
                Attempts++;

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
                Attempts++;

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
                Attempts++;

            NotifyOfPropertyChange(() => SelectedDiet);
            NotifyOfPropertyChange(() => CanSubmit);
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
        IEventAggregator eventAggregator,
        INotificationManager notificationManager
    )
    {
        _context = context;
        _request = request;
        _container = container;
        _eventAggregator = eventAggregator;
        _notificationManager = notificationManager;
    }

    public async Task SubmitAsync()
    {
        _timer.Stop();

        await _eventAggregator.PublishOnUIThreadAsync("Activity");

        await _request.UploadAsync(new Form
        {
            Anamnesis = Anamnesis,
            Diet = SelectedDiet.Name,
            Diagnosis = SelectedDiagnosis.Name,
            Department = SelectedDepartment.Name,
            Student = _container.GetInstance<Student>()
        });
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
        if (Minutes == 0 || Attempts >= Limit)
            _timer.Stop();
    }
}
