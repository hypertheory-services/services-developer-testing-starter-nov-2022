
using Alba;

using CoursesApi.IntegrationTests.CoursesResource.Fixtures;
using CoursesApi.Models;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace CoursesApi.IntegrationTests.CoursesResource;

public class GettingCourseOfferings : IClassFixture<CourseOfferingsFixture>
{
    private readonly IAlbaHost _host;
    private readonly WireMockServer _mockServer;
    public GettingCourseOfferings(CourseOfferingsFixture fixture)
    {
        _host = fixture.AlbaHost;
        _mockServer = fixture.MockServer;
    }

    [Fact]
    public async Task GettingOfferings()
    {
        var stubbedData = new Offerings
        {
            Data = new List<DateTime> { new DateTime(1969,4,20,23,59,00) }
        };
        _mockServer.Given(Request.Create()
            .WithPath("/2"))
            .RespondWith(
            Response.Create().WithBodyAsJson(stubbedData).WithStatusCode(System.Net.HttpStatusCode.OK));


        var offeringsResponse = await _host.Scenario(api =>
        {
            api.Get.Url("/courses/2/offerings");
            api.StatusCodeShouldBeOk();
        });

        var responseData = await offeringsResponse.ReadAsJsonAsync <Offerings>();

        Assert.NotNull(responseData);
        Assert.Equal(1969, responseData.Data.First().Year);

        _mockServer.ResetMappings(); // 
    }

    [Fact]
    public async Task GettingOfferingsForACourseThatDoesNotExist()
    {
        // we do not own a course #5, so the API should not be called, and a 404 shoud
        // be immediately returned.
        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/courses/5/offerings");
            api.StatusCodeShouldBe(404);
        });

        _mockServer.ResetMappings();
    }

    [Fact()]
    public async Task GettingNoOfferingsFromTheApi()
    {
        // we have that course, but when we call the API, it says there are no offerings
        // by returning a 404.
        // GET /1
      
        _mockServer.Given(Request.Create()
            .WithPath("/1"))
            .RespondWith(
            Response.Create().WithStatusCode(System.Net.HttpStatusCode.NotFound));


        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/courses/1/offerings");
            api.StatusCodeShouldBeOk();
        });
    }

    [Fact]
    public async Task AttachedResourceFailure()
    {
        _mockServer.Given(Request.Create()).RespondWith(
            Response.Create().
            WithStatusCode(System.Net.HttpStatusCode.InternalServerError) );

        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/courses/2/offerings");
            api.StatusCodeShouldBe(502);

        });

    }
}

