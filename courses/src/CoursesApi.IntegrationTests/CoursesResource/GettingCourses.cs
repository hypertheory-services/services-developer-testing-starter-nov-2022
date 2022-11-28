

using Alba;

using CoursesApi.IntegrationTests.CoursesResource.Fixtures;
using CoursesApi.Models;

namespace CoursesApi.IntegrationTests.CoursesResource;

public class GettingCourses : IClassFixture<CoursesResourceFixture>
{
    private readonly IAlbaHost _host;

    public GettingCourses(CoursesResourceFixture fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task GettingTheInitialCourseList()
    {
        // when I do a GET /courses
        // I get a 200 OK Status Response
        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/courses");
            api.StatusCodeShouldBeOk(); // 200 OK
        });

        var entity = await response.ReadAsJsonAsync<CoursesResponseModel>();

        entity = entity is not null ? entity : throw new ArgumentNullException(nameof(entity));

        Assert.Equal(0, entity.NumberOfFrontendCourses);
        Assert.Equal(0, entity.NumberOfBackendCourses);
        Assert.Empty(entity.Courses);

    }

}
