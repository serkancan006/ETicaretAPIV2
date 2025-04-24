using AutoMapper;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.UserAddress;
using ETicaretAPI.Application.Repositories;

namespace ETicaretAPI.Persistence.Services
{
    public class UserAddressService(IUserAddressReadRepository _userAddressReadRepository, IMapper _mapper) : IUserAddressService
    {
        public async Task<SingleUserAddress> GetByUserAddressAsync(string userAddressId)
        {
            var userAddress = await _userAddressReadRepository.GetByIdAsync(userAddressId);
            if (userAddress == null)
                throw new Exception("User address not found");

            return _mapper.Map<SingleUserAddress>(userAddress);
        }
    }
}
