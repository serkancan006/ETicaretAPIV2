using AutoMapper;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.UserAddress;
using ETicaretAPI.Persistence.Repositories;

namespace ETicaretAPI.Persistence.Services
{
    public class UserAddressService(UserAddressReadRepository userAddressReadRepository, IMapper mapper) : IUserAddressService
    {
        public async Task<SingleUserAddress> GetByUserAddressAsync(string userAddressId)
        {
            var userAddress = await userAddressReadRepository.GetByIdAsync(userAddressId);
            if (userAddress == null)
                throw new Exception("User address not found");

            return mapper.Map<SingleUserAddress>(userAddress);
        }
    }
}
