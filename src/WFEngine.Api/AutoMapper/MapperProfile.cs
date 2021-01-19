using AutoMapper;
using WFEngine.Api.Dto.Request.Auth;
using WFEngine.Api.Dto.Request.Project;
using WFEngine.Api.Dto.Request.Solution;
using WFEngine.Core.Entities;

namespace WFEngine.Api.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MapperProfile()
        {
            CreateMap<RegisterRequestDTO, Organization>().ForMember(vm => vm.Name, m => m.MapFrom(u => u.OrganizationName));
            CreateMap<RegisterRequestDTO, User>();
            CreateMap<InsertSolutionRequestDTO, Solution>();
            CreateMap<InsertProjectRequestDTO, Project>();
            CreateMap<UpdateProjectRequestDTO, Project>();
        }
    }
}
