﻿
using Alba;

using CoursesApi.IntegrationTests.CoursesResource.Fixtures;

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
        var stubbedData = new OfferingResponse
        {
            Data = new List<DateTime> { new DateTime(1969,4,20,23,59,00) }
        };
        _mockServer.Given(Request.Create()
            .WithPath("/18"))
            .RespondWith(
            Response.Create().WithBodyAsJson(stubbedData).WithStatusCode(System.Net.HttpStatusCode.OK));


        var offeringsResponse = await _host.Scenario(api =>
        {
            api.Get.Url("/courses/18/offerings");
            api.StatusCodeShouldBeOk();
        });

        var responseData = await offeringsResponse.ReadAsJsonAsync <OfferingResponse>();

        //Assert.NotNull(responseData);
        //Assert.Equal(1969, responseData.Data.First().Year);

        _mockServer.ResetMappings(); // 
    }

    [Fact(Skip ="when there is no course at that url")]
    public async Task GettingOfferingsForACourseThatDoesNotExist()
    {

    }

    [Fact(Skip ="When the offerings api return a 404")]
    public async Task GettingNoOfferingsFromTheApi()
    {

    }

    [Fact]
    public async Task AttachedResourceFailure()
    {
        _mockServer.Given(Request.Create()).RespondWith(
            Response.Create().
            WithStatusCode(System.Net.HttpStatusCode.InternalServerError) );

        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/courses/33/offerings");
            api.StatusCodeShouldBe(502);

        });

    }
}

public record OfferingResponse
{
    public List<DateTime> Data { get; set; } = new();
}