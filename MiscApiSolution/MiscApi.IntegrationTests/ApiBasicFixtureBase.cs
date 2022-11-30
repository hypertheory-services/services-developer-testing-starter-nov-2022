
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using MiscApi.Adapters;

using Moq;

namespace MiscApi.IntegrationTests;

public abstract class ApiBasicFixtureBase<T> : WebApplicationFactory<T> where T : class
{

    public ApiBasicFixtureBase()
    {
        
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
           SetupServices(services);
        });
    }

    protected abstract void SetupServices(IServiceCollection services);
    
}
