﻿using AutoMapper;
using IT_Talent_Program.Dtos;
using IT_Talent_Program.Entities;

namespace IT_Talent_Program.Helpers
{
    public class AutoMapperProf : Profile
    {
        public AutoMapperProf()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<UserUpdateDto, User>();
        }

    }
}
