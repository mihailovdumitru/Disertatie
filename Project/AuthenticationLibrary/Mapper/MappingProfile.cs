using AutoMapper;
using Model.Dto;
using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationLibrary.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, Token>();
        }
    }
}
