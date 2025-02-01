using Nis.WpfApp.Models;
using Nis.WpfApp.Requests;
using Nis.Core.Configuration;

namespace Nis.WpfApp.ViewModels;

[UsedImplicitly]
public class AnamnesesViewModel(CourseRequest request, IEventAggregator aggregator) : Screen
{
    private Assignment? _assignment;
    private BindableCollection<Assignment>? _assignments = [];

    public Assignment? Assignment
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

    public BindableCollection<Assignment>? Assignments
    {
        get => _assignments;
        set
        {
            _assignments = value;
            NotifyOfPropertyChange(() => Assignments);
        }
    }

    public async Task ActivityAsync()
    {
        await aggregator.PublishOnUIThreadAsync("Exam");
        await aggregator.PublishOnUIThreadAsync(Assignment);
    }

    protected override async void OnViewLoaded(object view)
    {
        await FetchAssignmentsAsync();

        base.OnViewLoaded(view);
    }

    private async Task FetchAssignmentsAsync() => Assignments = new((await request.GetCourseAsync(id: Convert.ToInt16(Settings.Configuration["Moodle:CourseId"])))?.Assignments);
}
