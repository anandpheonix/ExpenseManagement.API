using Application.Auth;
using Application.Controllers.Interfaces;
using Common;
using DataAccess.Repositories;
using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Controllers;

[ApiController]
[Route("api/auth/[action]")]
public class AuthController : Controller, IAuthController
{
    protected APIResponse _response;
    private readonly IUserRepository _userRepository;
    private readonly ITokenHandler _tokenHandler;

    public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
    {
        _userRepository = userRepository;
        _tokenHandler = tokenHandler;
        _response = new();
    }

    [HttpPost]
    [ActionName("token")]
    public async Task<IActionResult> GenerateToken(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.AuthenticateUser(request.UserName, request.Password);

        if (user.Item1 is not null)
        {
            var token = await _tokenHandler.GenerateToken(user.Item1);

            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = token;

            return Ok(_response);
        }

        _response.StatusCode = HttpStatusCode.BadRequest;
        _response.ErrorMessages = new List<string>() { user.Item2 };

        return BadRequest(_response);
    }
}
