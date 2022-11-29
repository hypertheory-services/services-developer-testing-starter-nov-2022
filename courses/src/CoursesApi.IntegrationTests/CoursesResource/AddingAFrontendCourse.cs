
using Alba;

using CoursesApi.IntegrationTests.CoursesResource.Fixtures;
using CoursesApi.Models;

namespace CoursesApi.IntegrationTests.CoursesResource;

public class AddingAFrontendCourse : IClassFixture<CoursesResourceFixture>
{
    private readonly IAlbaHost _host;

    public AddingAFrontendCourse(CoursesResourceFixture fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task AddingANewFrontendCourse()
    {
        var courseToAdd = new CourseCreateRequest
        {
            Title = "Angular Developer Testing",
            Description = "Test them real good!"
        };

        var addedCourseResponse = await _host.Scenario(api =>
        {
            // post the thing.
            api.Post.Json(courseToAdd).ToUrl("/frontend-courses");
            api.StatusCodeShouldBe(201);
            // the response status code should be 201
        });


        var addedCourse = await addedCourseResponse.ReadAsJsonAsync<CourseItemDetailsResponse>();

        // did it map the stuff right.
        Assert.Equal(courseToAdd.Title, addedCourse.Title);
        Assert.Equal(courseToAdd.Description, addedCourse.Description);

        Assert.Equal(Domain.CategoryType.Frontend, addedCourse.Category);
        Assert.Equal("1", addedCourse.Id);

        var locationHeader = addedCourseResponse.Context.Response.Headers["Location"].SingleOrDefault() ?? "NO LOCATION HEADER";
        // Has a location header
        // is the path host:port/courses/{id}
        var locationPath = new Uri(locationHeader).PathAndQuery; // /courses/1
        // Has an entity

        var getAddedCourseResponse = await _host.Scenario(api =>
        {
            api.Get.Url(locationPath);
            api.StatusCodeShouldBeOk();
        });

        var addedCourseEntity = await getAddedCourseResponse.ReadAsJsonAsync<CourseItemDetailsResponse>();

        Assert.Equal(addedCourseEntity, addedCourse);

    }
}
