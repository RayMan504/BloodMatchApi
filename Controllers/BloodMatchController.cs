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

    [HttpGet("{role}/{bloodType}")]
    public IActionResult Check(Role role, BloodType bloodType)
    {
        _logger.LogInformation("Check requested with bloodType='{BloodType}'", bloodType);

        if (!Enum.IsDefined<BloodType>(bloodType) && !Enum.IsDefined<Role>(role))
        {
            _logger.LogWarning("Invalid parameters: bloodType='{BloodType}', role='{Role}'", bloodType, role);
            return Error("A valid blood type and role must be provided.", 400);
        }

        var result = _service.GetBloodTypeMatch(role, bloodType);
        return OkEnvelope(new { match = result });
    }
}