using SmartMenu.Services.AuthAPI.Models;

namespace SmartMenu.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
