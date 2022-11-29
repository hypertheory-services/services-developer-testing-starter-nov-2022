

using Alba;

using CoursesApi.Adapters;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WireMock.Server;

namespace CoursesApi.IntegrationTests.CoursesResource.Fixtures;

public class CourseOfferingsFixture : IAsyncLifetime
{

    private readonly string SQL_IMAGE = "jeffrygonzalez/sdt-nov-2022-sql:20221115192103_Initial-seeded";


    public IAlbaHost AlbaHost = null!;
    public WireMockServer MockServer = null!;
    private readonly TestcontainerDatabase _sqlContainer;

    public CourseOfferingsFixture()
    {
        _sqlContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
    .WithDatabase(new MsSqlTestcontainerConfiguration
    {
        Database = "courses-dev",
        Password = "TokyoJoe138!"
    }).WithImage(SQL_IMAGE).Build();
    }
    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
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
                // ephemeral version of the database that will be used just for this test
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CoursesDataContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                else
                {
                    throw new ArgumentNullException("There is no Service Configured");
                }

                services.AddDbContext<CoursesDataContext>(options =>
                {
                    options.UseSqlServer(_sqlContainer.ConnectionString);
                });

            });
        });

    }
    public async Task DisposeAsync()
    {
        await _sqlContainer.StopAsync();
        MockServer.Stop();
        await AlbaHost.DisposeAsync();
    }
}
