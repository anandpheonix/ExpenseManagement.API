using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Interfaces;

public interface IAuthController
{
    Task<IActionResult> GenerateToken(LoginRequest request, CancellationToken cancellationToken);
}
