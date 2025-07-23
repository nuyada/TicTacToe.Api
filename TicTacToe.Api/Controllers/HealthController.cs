using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult CheckHealth() => Ok("API is running");
}
