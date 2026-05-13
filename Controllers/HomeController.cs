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
            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = BloodType.ABN.ToString(), Text = BloodType.ABN.ToString() },
                new SelectListItem { Value = BloodType.ABP.ToString(), Text = BloodType.ABP.ToString() },
                new SelectListItem { Value = BloodType.AP.ToString(), Text = BloodType.AP.ToString() },
                new SelectListItem { Value = BloodType.AN.ToString(), Text = BloodType.AN.ToString() },
                new SelectListItem { Value = BloodType.BP.ToString(), Text = BloodType.BP.ToString() },
                new SelectListItem { Value = BloodType.BN.ToString(), Text = BloodType.BN.ToString() },
                new SelectListItem { Value = BloodType.OP.ToString(), Text = BloodType.OP.ToString() },
                new SelectListItem { Value = BloodType.ON.ToString(), Text = BloodType.ON.ToString() },
            };

            // pass the list of items to the view
            ViewBag.BloodTypes = items;
            return View();
        }
    }