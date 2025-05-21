using BizFlow.Domain.Model.Auth;

namespace BizFlow.Application.AuthServices;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginModel model);
    Task<RegistrationResponse> Register(RegistrationModel model);
}
