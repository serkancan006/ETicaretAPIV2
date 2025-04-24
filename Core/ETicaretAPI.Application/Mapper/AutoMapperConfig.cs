using AutoMapper;
using ETicaretAPI.Application.DTOs.Category;
using ETicaretAPI.Application.DTOs.UserAddress;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<MainCategory, ListCategory>().ReverseMap();
            CreateMap<MainCategory, UpdateMainCategory>().ReverseMap();
            CreateMap<MainCategory, CreateMainCategory>().ReverseMap();

            CreateMap<SubCategory, ListSubCategory>().ReverseMap();
            CreateMap<SubCategory, UpdateSubCategory>().ReverseMap();
            CreateMap<SubCategory, CreateSubCategory>().ReverseMap();


            CreateMap<SingleUserAddress, UserAddress>().ReverseMap();
        }
    }
}
