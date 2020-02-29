using API.ViewModels;
using Business.Model;
using AutoMapper;

namespace API.MappingProfiles
{
    public class BusinessToDtoProfile : Profile
    {
        public BusinessToDtoProfile()
        {
            CreateMap<Poll, PollDto>();
            CreateMap<Option, OptionDto>();
        }
    }
}
