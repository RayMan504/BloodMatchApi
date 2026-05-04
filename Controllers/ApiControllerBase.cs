using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloodMatchApi.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ILogger _logger;

    protected ApiControllerBase(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns a standardized OK response with an envelope object.
    /// </summary>
    protected IActionResult OkEnvelope(object? data = null)
    {
        return Ok(new { success = true, data });
    }

    /// <summary>
    /// Returns a standardized error response.
    /// </summary>
    protected IActionResult Error(string message, int statusCode = 400)
    {
        var payload = new { success = false, error = message };
        return StatusCode(statusCode, payload);
    }
}
