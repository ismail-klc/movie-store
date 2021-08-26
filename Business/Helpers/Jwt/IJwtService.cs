using System.IdentityModel.Tokens.Jwt;

namespace Business.Helpers.Jwt
{
    public interface IJwtService
    {
        string Generate(int id, string role);
        JwtSecurityToken Verify(string jwt);
    }
}