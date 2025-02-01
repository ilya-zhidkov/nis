using Nis.WpfApp.Models;

namespace Nis.WpfApp.Requests;

[UsedImplicitly]
public class CourseRequest : BaseRequest
{
    public async Task<Course?> GetCourseAsync(short id) => await GetAsync<Course>(
        uri: $"{Endpoint}/courses/{id}",
        headers: new Dictionary<string, string> { { "token", Headers["Authorization"]! } }
    );
}
