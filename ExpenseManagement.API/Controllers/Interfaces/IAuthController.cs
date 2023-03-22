using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Interfaces;

public interface IAuthController
{
    Task<IActionResult> Login();
}
