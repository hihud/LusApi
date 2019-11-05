using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LusCore.Product;
using LusCore.User;

namespace LusCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, ProductModel>();
            CreateMap<UserDto, UserModel>().ReverseMap();
        }
    }
}
