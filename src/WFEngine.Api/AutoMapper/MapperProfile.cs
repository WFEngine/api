using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WFEngine.Api.Dto.Request.Auth;
using WFEngine.Api.Dto.Request.Project;
using WFEngine.Api.Dto.Request.Solution;
using WFEngine.Api.Dto.Request.WFObject;
using WFEngine.Api.Dto.Response.Auth;
using WFEngine.Api.Dto.Response.Project;
using WFEngine.Api.Dto.Response.Solution;
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
            CreateMap<InsertWFObjectRequestDTO, WFObject>();
            CreateMap<Organization, LoginResponse>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Name));
            CreateMap<User, LoginResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(dest => dest.EmailVerificated, opt => opt.MapFrom(src => src.EmailVerificated))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId));
            CreateMap<Organization, User>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Organization, RegisterResponse>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.Id));
            CreateMap<User, RegisterResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Organization, GetUserResponse>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Name));
            CreateMap<User, GetUserResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(dest => dest.EmailVerificated, opt => opt.MapFrom(src => src.EmailVerificated))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId));
            CreateMap<Solution, Project>()
                .ForMember(dest => dest.SolutionId, opt => opt.MapFrom(src => src.Id))
                .IgnoreKeys(new List<string> {"SolutionId" })
                .IgnorePrimaryKey();

            CreateMap<User, Project>()
                .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.Id))
                .IgnoreKeys(new List<string> { "CreatorId"})
                .IgnorePrimaryKey();
            CreateMap<Project, InsertProjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .IgnoreKeys(new List<string> { "Id"})
                .IgnorePrimaryKey();
            CreateMap<UpdateProjectRequestDTO, Project>();
            CreateMap<User, Solution>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.OrganizationId))
                .IgnoreKeys(new List<string> { "OrganizationId" });
            CreateMap<Solution, InsertSolutionResponse>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MapperExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination>
               IgnorePrimaryKey<TSource, TDestination>(
                   this IMappingExpression<TSource, TDestination> expression)
        {
            var desType = typeof(TDestination);
            foreach (var property in desType.GetProperties().Where(p =>p.Name=="Id"))
            {
                expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="expression"></param>
        /// <param name="includeItems"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination>
                   IgnoreKeys
            <TSource, TDestination>(
                       this IMappingExpression<TSource, TDestination> expression,List<string> includeItems)
        {
            var desType = typeof(TDestination);
            foreach (var property in desType.GetProperties().Where(p =>!includeItems.Contains(p.Name)))
                expression.ForMember(property.Name, opt => opt.Ignore());


            return expression;
        }
    }
}
