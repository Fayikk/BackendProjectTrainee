using AutoMapper;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Entity.Model;

namespace MulakatCalisma.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
