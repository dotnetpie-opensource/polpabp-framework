using System;
using AutoMapper;
using PolpAbp.Framework.Auditing.Dto;
using PolpAbp.Framework.Identity.Dto;
using Volo.Abp.AuditLogging;
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

            CreateMap<AuditLog, AuditLogListDto>()
                .ForMember(dst => dst.Parameters, o => o.Ignore())
                .ForMember(dst => dst.ServiceName, o => o.Ignore())
                .ForMember(dst => dst.CustomData, o => o.Ignore())
                .ForMember(dst => dst.MethodName, o => o.Ignore());

            CreateMap<EntityChange, EntityChangeListDto>()
                .ForMember(dst => dst.UserId, o => o.Ignore())
                .ForMember(dst => dst.UserName, o => o.Ignore())
                .ForMember(dst => dst.EntityChangeSetId, o => o.Ignore());

            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

        }
    }
}
