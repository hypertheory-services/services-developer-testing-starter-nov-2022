

using System.Net;
using System.Net.Http.Json;

using Microsoft.Extensions.DependencyInjection;

using MiscApi.Adapters;
using MiscApi.Controllers;

using Moq;

namespace MiscApi.IntegrationTests;

public class InfoResourceTests : IClassFixture<InfoResourceTest>
{

    public readonly HttpClient _client;

    public InfoResourceTests(InfoResourceTest fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task GettingApiInfo()
    {
        var response = await _client.GetAsync("/server-info");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

        var serverInfo =  await response.Content.ReadFromJsonAsync<ServerInfo>();

        Assert.NotNull(serverInfo);
        var expectedDate = new DateTime(1969, 4, 20, 23, 59, 00);
        Assert.Equal(expectedDate, serverInfo.LastChecked);
    }
}

// Fixtures are the "Context" - a part of the "Given"

public class InfoResourceTest : ApiBasicFixtureBase<Program>
{
    protected override void SetupServices(IServiceCollection services)
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrent()).Returns(new DateTime(1969, 4, 20, 23, 59, 00));
        services.AddSingleton<ISystemTime>(stubbedClock.Object);
       
    }
}