using BizFlow.Application.AuthServices;
using BizFlow.Domain.Model.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BizFlow.Controllers.Account;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAuthService authService) : ControllerBase
{

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel model)
    {
        var response = await authService.Login(model);
        Console.WriteLine(response);
        if (response is null)
            throw new UnauthorizedAccessException("Invalid username or password. Please try again.");
        return Ok(response);
    }


    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationModel model)
    {
        return Ok(await authService.Register(model));
    }
}
