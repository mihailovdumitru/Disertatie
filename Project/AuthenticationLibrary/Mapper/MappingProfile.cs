using AutoMapper;
using Model.Dto;
using Model.Repositories;

namespace AuthenticationLibrary.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, Token>();
        }
    }
}