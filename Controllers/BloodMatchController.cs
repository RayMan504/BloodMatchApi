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

    [HttpGet("types")]
    public IActionResult GetValidBloodTypes()
    {
        _logger.LogInformation("Valid blood types requested");
        var types = _service.GetSupportedBloodTypes();
        return OkEnvelope(new { types });
    }

    [HttpGet("{bloodType}")]
    public IActionResult Check(BloodType bloodType)
    {
        _logger.LogInformation("Check requested with bloodType='{BloodType}'", bloodType);

        if (!Enum.IsDefined<BloodType>(bloodType))
        {
            _logger.LogWarning("Invalid parameters: bloodType='{BloodType}'", bloodType);
            return Error("A blood type must be provided.", 400);
        }

        var result = _service.GetBloodTypeMatch(bloodType);
        return OkEnvelope(new { match = result });
    }
}