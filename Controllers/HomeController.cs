using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloodMatchApi.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ApiControllerBase
    {
    public HomeController(ILogger<HomeController> logger) : base(logger)
    {
    }

    [HttpGet]
        public IActionResult Get()
        {
            var items = new[]
            {
                new { id = 1, name = BloodType.ABN.ToString() },
                new { id = 2, name = BloodType.ABP.ToString() },
                new { id = 3, name = BloodType.AP.ToString() },
                new { id = 4, name = BloodType.AN.ToString() },
                new { id = 5, name = BloodType.BP.ToString() },
                new { id = 6, name = BloodType.BN.ToString() },
                new { id = 7, name = BloodType.OP.ToString() },
                new { id = 8, name = BloodType.ON.ToString() },
            };

            return Ok(items);
        }
    }