using System.Text.Json;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.Requests;

[UsedImplicitly]
public class CourseRequest : BaseRequest
{
    public async Task<Course?> GetCourseAsync(ushort id)
    {
        Course? course;

        try
        {
            course = await GetAsync<Course>(
                uri: $"{Endpoint}/courses/{id}",
                headers: new Dictionary<string, string> { { "Authorization", Headers["Authorization"] } }
            );
        }
        catch (JsonException)
        {
            return null;
        }

        return course;
    }
}
