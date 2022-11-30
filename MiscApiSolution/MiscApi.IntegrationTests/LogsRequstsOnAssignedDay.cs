
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using MiscApi.Adapters;
using Moq;

namespace MiscApi.IntegrationTests;

public class LogsRequestsOnAssignedDay : IClassFixture<WednesdayFixture>
{

    private HttpClient _client;
    private Mock<IProvideLogging> _loggerMock;
    public LogsRequestsOnAssignedDay(WednesdayFixture factory)
    {
        _client = factory.CreateDefaultClient();
        _loggerMock = factory.LoggerMock;
    }

    [Fact]
    public async Task ShouldOnlyLogOnWednesday()
    {
        await _client.GetAsync("/server-info");

        _loggerMock.Verify(f => f.LogInformationalMessage("ServerInfoController", "Request on Wednesday"));
    }
}

public class DoesNotLogRequestOnUnassignedDays : IClassFixture<NotWednesdayFixture>
{
    private HttpClient _client;
    private Mock<IProvideLogging> _loggerMock;
    private Mock<ISystemTime> _clockStub;

    public DoesNotLogRequestOnUnassignedDays(NotWednesdayFixture fixture)
    {
        _client = fixture.CreateDefaultClient();
        _loggerMock = fixture.LoggerMock;
        _clockStub = fixture.SystemTimeStub;
    }
    [Theory]
    [MemberData(nameof(Data))]
    public async Task ShouldNotLogOn(DateTime dateOfRequest)
    {
        _clockStub.Setup(c => c.GetCurrent()).Returns(dateOfRequest);

        await _client.GetAsync("/server-info");

        _loggerMock.Verify(v => v.LogInformationalMessage(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[] { new DateTime(2022, 12, 1, 1, 1, 1) };
        yield return new object[] { new DateTime(2022, 12, 2, 1, 1, 1) };
        yield return new object[] { new DateTime(2022, 12, 3, 1, 1, 1) };

    }
}

public class WednesdayFixture : ApiBasicFixtureBase<Program>
{
    public Mock<IProvideLogging> LoggerMock = null!;
    protected override void SetupServices(IServiceCollection services)
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrent()).Returns(new DateTime(2022, 11, 30, 23, 59, 00));

        LoggerMock = new Mock<IProvideLogging>();
        services.AddSingleton<ISystemTime>(stubbedClock.Object);
        services.AddScoped<IProvideLogging>(sp => LoggerMock.Object);
    }
}

public class NotWednesdayFixture : ApiBasicFixtureBase<Program>
{
    public Mock<IProvideLogging> LoggerMock = null!;
    public Mock<ISystemTime> SystemTimeStub = null;
    protected override void SetupServices(IServiceCollection services)
    {
        SystemTimeStub = new Mock<ISystemTime>();
       

        LoggerMock = new Mock<IProvideLogging>();
        services.AddSingleton<ISystemTime>(SystemTimeStub.Object);
        services.AddScoped<IProvideLogging>(sp => LoggerMock.Object);
    }
}
