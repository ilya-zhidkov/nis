using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.Core.Persistence;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;

namespace Nis.WpfApp.ViewModels;

public class TestComboViewModel : Screen
{
    private readonly DataContext _context;
    private readonly IWindowManager _window;
    private readonly SimpleContainer _container;
    private byte _attempts;
    private double _timeLeft;
    private Diet _selectedDiet;
    private const byte _limit = 3;
    private DispatcherTimer _timer;
    private const byte Seconds = 30;
    private Diagnosis _selectedDiagnosis;
    private Department _selectedDepartment;
    private BindableCollection<Diet> _diets;
    private BindableCollection<Diagnosis> _diagnoses;
    private BindableCollection<Department> _departments;

    public double TimeLeft
    {
        get => _timeLeft;
        set
        {
            _timeLeft = value;
            NotifyOfPropertyChange(() => TimeLeft);
            NotifyOfPropertyChange(() => CanSubmit);
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

    public TestComboViewModel(DataContext context, SimpleContainer container, IWindowManager window)
    {
        _window = window;
        _context = context;
        _container = container;
    }

    public async Task SubmitAsync()
    {
        await _window.ShowWindowAsync(_container.GetInstance<TestCheckViewModel>());

        _timer.Stop();
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
        TimeLeft = Seconds;

        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
        _timer.Tick += timer_Tick;
        _timer.Start();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        if (--TimeLeft == 0 || Attempts >= Limit)
            _timer.Stop();
    }
}
