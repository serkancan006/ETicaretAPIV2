using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Task<DTOs.Token> CreateAccessTokenAsync(int second, AppUser appUser);
        string CreateRefreshToken();
    }
}
