using AutoMapper;
using User.Api.Models;
using User.Api.Repositories.Entities;

namespace User.Api
{
    /// <summary>
    /// Provide AutoMapper mapping configuration for the models and entities.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest,UserEntity>();
            CreateMap<UserEntity, Models.User>();
        }
    }
}