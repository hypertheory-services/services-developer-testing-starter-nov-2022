

using Alba;

using Microsoft.Extensions.Configuration;

using WireMock.Server;

namespace CoursesApi.IntegrationTests.CoursesResource.Fixtures;

public class CourseOfferingsFixture : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null!;
    public WireMockServer MockServer = null!;
    public async Task InitializeAsync()
    {
        MockServer = WireMockServer.Start();
        AlbaHost = await Alba.AlbaHost.For<global::Program>(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new KeyValuePair<string, string>[] {
                    new("courses-api", MockServer.Urls[0])

                });
            });

            builder.ConfigureServices(services =>
            {
                // Replace the datacontext with one that points to another 

            });
        });

    }
    public async Task DisposeAsync()
    {
        MockServer.Stop();
        await AlbaHost.DisposeAsync();
    }
}
