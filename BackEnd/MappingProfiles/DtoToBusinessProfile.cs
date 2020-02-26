using API.ViewModels.Output;
using Business.Model;
using AutoMapper;

namespace API.MappingProfiles
{
    public class DtoToBusinessProfile : Profile
    {
        public DtoToBusinessProfile()
        {
            CreateMap<PollDto, Poll>();
            CreateMap<OptionDto, Option>();
        }
    }
}
