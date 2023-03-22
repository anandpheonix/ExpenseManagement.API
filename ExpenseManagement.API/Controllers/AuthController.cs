using Application.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_SetupCheck.Controllers;

[ApiController]
[Route("auth/[action]")]
public class AuthController : Controller, IAuthController
{
    public AuthController()
    {
    }

    [HttpPost]
    [ActionName("login")]
    public async Task<IActionResult> Login()
    {
        await Task.Delay(1);
        return Ok();
    }
}
