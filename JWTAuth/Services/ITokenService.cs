using JWTAuth.Models;
namespace JWTAuth.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, IList<string> roles);
    }
}
