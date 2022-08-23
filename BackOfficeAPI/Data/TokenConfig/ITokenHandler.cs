using BackOfficeAPI.Models;

namespace BackOfficeAPI.Data.TokenConfig
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int day, Candidat candidat);
        Token CreateAccessToken(int day, User user);
        string CreateRefreshToken();
    }
}