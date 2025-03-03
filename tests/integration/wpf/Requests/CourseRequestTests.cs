using Nis.WpfApp.Requests;
using Nis.Core.Configuration;

namespace Nis.WpfApp.IntegrationTests.Requests;

public sealed class CourseRequestTests : BaseIntegrationTest
{
    private readonly SignInRequest _request = new(container: new());

    [Fact]
    public async Task it_should_return_a_course_when_a_valid_course_id_is_passed()
    {
        await _request.SignInAsync(Settings.Configuration["Moodle:Credentials:Username"]!, Settings.Configuration["Moodle:Credentials:Password"]!);

        Assert.NotNull(await new CourseRequest().GetCourseAsync(Convert.ToUInt16(Settings.Configuration["Moodle:CourseId"])));
    }

    [Theory]
    [InlineData(ushort.MinValue)]
    [InlineData(ushort.MaxValue)]
    public async Task it_should_return_null_when_an_invalid_course_id_is_passed(ushort id)
    {
        await _request.SignInAsync(Settings.Configuration["Moodle:Credentials:Username"]!, Settings.Configuration["Moodle:Credentials:Password"]!);

        Assert.Null(await new CourseRequest().GetCourseAsync(id));
    }
}
