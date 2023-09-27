using AutoMapper;
using PU.Core.DTO.Request;
using PU.Core.Models;

namespace PU.WebApi.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<GroupRequest, Group>();
            CreateMap<PermissionRequest, Permission>();
        }
    }
}
