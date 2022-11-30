using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using MiscApi.Adapters;

namespace MiscApi.Controllers;

public class ServerInfoController : ControllerBase
{
    private readonly ISystemTime _clock;

    public ServerInfoController(ISystemTime clock)
    {
        _clock = clock;
    }

    [HttpGet("/server-info")]
    public async Task<ActionResult> GetServerInfo([FromServices] IProvideLogging _logger)
    {
        var time = _clock.GetCurrent();
        var response = new ServerInfo
        {
            LastChecked = _clock.GetCurrent()
        };
        if(time.DayOfWeek== DayOfWeek.Wednesday) 
        {
            _logger.LogInformationalMessage(nameof(ServerInfoController), "Request on Wednesday");
        }
        return Ok(response);
    }
}

public record ServerInfo {
    public DateTime LastChecked { get; init; }
   
}