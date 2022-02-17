using System;
using AutoMapper;
using PolpAbp.Framework.Identity.Dto;
using Volo.Abp.Identity;

namespace PolpAbp.Framework
{
    public class FrameworkApplicationAutoMapperProfile : Profile
    {
        public FrameworkApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<IdentityRoleDto, IdentityRoleAdaptorDto>();

        }
    }
}
