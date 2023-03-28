using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.IControllers;

public interface IAuthController
{
    Task<IActionResult> GenerateToken(LoginRequest request, CancellationToken cancellationToken);
}
