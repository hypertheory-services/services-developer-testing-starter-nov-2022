

using Alba;

using CoursesApi.IntegrationTests.CoursesResource.Fixtures;

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
        await _host.Scenario(api =>
        {
            api.Get.Url("/coursexs");
            api.StatusCodeShouldBeOk(); // 200 OK
        });

    }

}
