using API.ViewModels.Output;
using Business.Model;
using AutoMapper;

namespace API.MappingProfiles
{
    public class DtoToBusinessProfile : Profile
    {
        public DtoToBusinessProfile()
        {
            CreateMap<PollOutput, Poll>();
            CreateMap<OptionOutput, Option>();
        }
    }
}
