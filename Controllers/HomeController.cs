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
            
            return Content("BloodMatch API is running. Use /api/match/{bloodtype} to find blood matches. For valid blood types use /api/match/types");
        }
    }