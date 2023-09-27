using AutoMapper;
using PU.Core.Models;

namespace PU.DataAccess.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<User, Entities.User>()
                .ReverseMap();

            CreateMap<Permission, Entities.Permission>()
                .ReverseMap();

            CreateMap<Group, Entities.Group>()
                .ReverseMap();
        }
    }
}
