using AutoMapper;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using Nis.Core.Models.Enums;
using Nis.Core.Persistence;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class MalnutritionViewModel : Screen
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IEventAggregator _aggregator;
    private BindableCollection<MedicalScale> _scales;

    public BindableCollection<MedicalScale> Scales
    {
        get => _scales;
        set
        {
            _scales = value;
            NotifyOfPropertyChange(() => Scales);
        }
    }

    public MalnutritionViewModel(
        IMapper mapper,
        DataContext context,
        IEventAggregator aggregator
    )
    {
        _mapper = mapper;
        _context = context;
        _aggregator = aggregator;
    }

    public async Task Activity() => await _aggregator.PublishOnUIThreadAsync("Activity");

    public async Task Decubitus() => await _aggregator.PublishOnUIThreadAsync("Decubitus");

    public async Task Malnutrition() => await _aggregator.PublishOnUIThreadAsync("Malnutrition");

    public async Task Fall() => await _aggregator.PublishOnUIThreadAsync("Fall");

    protected override async void OnViewLoaded(object view)
    {
        Scales = _mapper.Map<BindableCollection<MedicalScale>>(
            await _context.MedicalScales
                .Include(scale => scale.Activities)
                .Where(scale => scale.ScaleCategory == MedicalScaleCategory.NutritionalStatusAssessmentScreening)
                .Union(_context.MedicalScales
                    .Include(scale => scale.Activities)
                    .Where(scale => scale.ScaleCategory == MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination)
                )
                .ToListAsync()
        );
    }
}
