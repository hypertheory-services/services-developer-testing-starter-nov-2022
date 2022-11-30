namespace MiscApi.Adapters;

public interface IProvideLogging
{
    void LogInformationalMessage(string source, string message);
}

public class LoggingAdapter : IProvideLogging
{
    private readonly ILogger<LoggingAdapter> _logger;

    public LoggingAdapter(ILogger<LoggingAdapter> logger)
    {
        _logger = logger;
    }

    public void LogInformationalMessage(string source, string message)
    {
        _logger.LogInformation($"At {source}: {message}");
    }
}
