using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.WpfApp.Requests;

namespace Nis.WpfApp.ViewModels;

public class AnamnesesViewModel : Screen
{
    private Assignment _assignment;
    private readonly CourseRequest _request;
    private readonly IEventAggregator _aggregator;
    private BindableCollection<Assignment> _assignments;

    public Assignment Assignment
    {
        get => _assignment;
        set
        {
            _assignment = value;
            NotifyOfPropertyChange(() => Assignment);
            NotifyOfPropertyChange(() => CanStartExam);
        }
    }

    public bool CanStartExam => Assignment is not null;

    public BindableCollection<Assignment> Assignments
    {
        get => _assignments;
        set
        {
            _assignments = value;
            NotifyOfPropertyChange(() => Assignments);
        }
    }

    public AnamnesesViewModel(CourseRequest request, IEventAggregator aggregator)
    {
        _request = request;
        _aggregator = aggregator;
    }

    public async Task Activity()
    {
        await _aggregator.PublishOnUIThreadAsync("Exam");
        await _aggregator.PublishOnUIThreadAsync(Assignment);
    }

    protected override async void OnViewLoaded(object view)
    {
        await FetchAssignmentsAsync();

        base.OnViewLoaded(view);
    }

    private async Task FetchAssignmentsAsync() => Assignments = new BindableCollection<Assignment>((await _request.GetCourseAsync(id: 5))?.Assignments);
}
