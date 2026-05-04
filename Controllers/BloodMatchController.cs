using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloodMatchApi.Controllers;

[Route("api/[controller]")]
public class MatchController : ApiControllerBase
{
    private readonly BloodMatchService _service;

    public MatchController(BloodMatchService service, ILogger<MatchController> logger)
        : base(logger)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Check(string donor, string recipient)
    {
        _logger.LogInformation("Check requested with donor='{Donor}' recipient='{Recipient}'", donor, recipient);

        if (string.IsNullOrWhiteSpace(donor) || string.IsNullOrWhiteSpace(recipient))
        {
            _logger.LogWarning("Invalid parameters: donor='{Donor}', recipient='{Recipient}'", donor, recipient);
            return Error("Both donor and recipient blood types must be provided.", 400);
        }

        var result = _service.IsMatch(donor, recipient);
        return OkEnvelope(new { match = result });
    }
}