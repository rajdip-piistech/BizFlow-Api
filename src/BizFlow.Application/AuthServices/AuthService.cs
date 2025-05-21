using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BizFlow.Domain.Model.Auth;
using BizFlow.Domain.Model.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BizFlow.Application.AuthServices;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityModel.User> _userManager;
    private readonly SignInManager<IdentityModel.User> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<IdentityModel.User> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<IdentityModel.User> signInManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
    }

    public async Task<LoginResponse> Login(LoginModel request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new UnauthorizedAccessException($"Invalid username or password. Please try again.");
        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException($"Invalid username or password. Please try again.");
        }

        var jwtSecurityToken = await GenerateToken(user);

        var response = new LoginResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

  

    public async Task<RegistrationResponse> Register(RegistrationModel request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);

        if (existingUser != null)
        {
            throw new Exception($"Username '{request.UserName}' already exists.");
        }

        var user = new IdentityModel.User
        {
            Email = request.Email,
            UserName = request.UserName,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()

        };

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
        else
        {
            throw new Exception($"Email {request.Email} already exists.");
        }
    }

    

    private async Task<JwtSecurityToken> GenerateToken(IdentityModel.User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("UserId", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        }.Union(userClaims).Union(roleClaims);

        Console.WriteLine(claims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}
