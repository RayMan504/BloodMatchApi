using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace BloodMatchApi.Controllers;

[ApiController]
[Route("/")]
public class HomeController : Controller
    {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
        public IActionResult Index()
        {
            // for simplicity, we will just return a message with instructions on how to use the API
            return Content("Welcome! BloodMatch API is running after latest deployment. Use /api/match/donor_or_recipient/target_blood_type to find blood matches. To view valid blood types list use /api/match/types");
        }
    }