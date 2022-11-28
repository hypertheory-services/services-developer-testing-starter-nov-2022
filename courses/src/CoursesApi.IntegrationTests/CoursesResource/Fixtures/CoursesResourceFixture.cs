
using Alba;

namespace CoursesApi.IntegrationTests.CoursesResource.Fixtures;

public class CoursesResourceFixture : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null!;
    public async Task InitializeAsync()
    {
        AlbaHost = await Alba.AlbaHost.For<global::Program>(builder =>
        {

        });
    }
    public async Task DisposeAsync()
    {
       await AlbaHost.DisposeAsync();
    }

}
