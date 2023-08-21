using AutoMapper;
using PoqAssignment.Application.DTO;
using PoqAssignment.Application.Helpers;
using PoqAssignment.Domain.Models.Filters;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Infrastructure.Repositories.Profiles
{
    public class MockyProfile : Profile
    {
        public MockyProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<FilterByUser, UserFilter>()
                .ForMember(dest => dest.Highlight, opt => opt.MapFrom<ColorResolver>());
        }
    }
}