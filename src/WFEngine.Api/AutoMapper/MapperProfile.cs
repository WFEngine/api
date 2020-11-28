using AutoMapper;
using WFEngine.Api.Dto.Request.Auth;
using WFEngine.Core.Entities;

namespace WFEngine.Api.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterRequestDTO, Organization>().ForMember(vm => vm.Name, m => m.MapFrom(u => u.OrganizationName));
            CreateMap<RegisterRequestDTO, User>();
        }
    }
}
