using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Response, ResponseDto>();
        }
    }
}
