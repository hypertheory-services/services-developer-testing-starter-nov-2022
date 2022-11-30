
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using MiscApi.Adapters;

using Moq;

namespace MiscApi.IntegrationTests;

public class MiscApiBasicFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var stubbedClock = new Mock<ISystemTime>();
            stubbedClock.Setup(c => c.GetCurrent()).Returns(new DateTime(1969, 4, 20, 23, 59, 00));
            services.AddSingleton<ISystemTime>(stubbedClock.Object);
        });
    }
}
