﻿using API.ViewModels.Output;
using Business.Model;
using AutoMapper;

namespace API.MappingProfiles
{
    public class BusinessToDtoProfile : Profile
    {
        public BusinessToDtoProfile()
        {
            CreateMap<Poll, PollOutput>();
            CreateMap<Option, OptionOutput>();
        }
    }
}
