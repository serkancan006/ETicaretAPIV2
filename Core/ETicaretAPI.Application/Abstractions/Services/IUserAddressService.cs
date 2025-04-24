using ETicaretAPI.Application.DTOs.UserAddress;
namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IUserAddressService
    {
        Task<SingleUserAddress> GetByUserAddressAsync(string userAddressId);
    }
}
