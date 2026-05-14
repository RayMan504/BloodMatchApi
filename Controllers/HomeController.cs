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
            // trigger prompt to allow user to identify as Donor or Recipient
            Console.WriteLine("Are you a Donor or Recipient?:");
            Console.WriteLine("1. Donor");
            Console.WriteLine("2. Recipient");

            string? role = Console.ReadLine();
            Console.WriteLine($"You selected: {role}");
            // assign role to session and redirect to appropriate page
            // for simplicity, we will just return a message with instructions on how to use the API
            return Content("BloodMatch API is running after latest deployment. Use /api/match/{bloodtype} to find blood matches. For valid blood types use /api/match/types");
        }
    }