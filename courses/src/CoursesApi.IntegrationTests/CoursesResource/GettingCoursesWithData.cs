using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alba;

using CoursesApi.IntegrationTests.CoursesResource.Fixtures;
using CoursesApi.Models;

namespace CoursesApi.IntegrationTests.CoursesResource;

public class GettingCoursesWithData : IClassFixture<CoursesSeededResourceFixture>
{

    private readonly IAlbaHost _host;
    public GettingCoursesWithData(CoursesSeededResourceFixture fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task GetTheSeededCoursesList()
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

        Assert.Equal(1, entity.NumberOfFrontendCourses);
        Assert.Equal(2, entity.NumberOfBackendCourses);
        // whatever you feel you need to do here..Assert.Empty(entity.Courses);
        Assert.Equal(3, entity.Courses.Count());

    }
}
