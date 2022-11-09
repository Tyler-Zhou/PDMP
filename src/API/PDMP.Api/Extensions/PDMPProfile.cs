using AutoMapper;
using PDMP.Contract;
using PDMP.Domain;

namespace PDMP.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class PDMPProfile : MapperConfigurationExpression
    {
        /// <summary>
        /// 
        /// </summary>
        public PDMPProfile()
        {
            //Permission
            CreateMap<PermissionEntity, PermissionInfo>().ReverseMap();
            CreateMap<PagedList<PermissionEntity>, PagedList<PermissionInfo>>().ReverseMap();
            //Role
            CreateMap<RoleEntity, RoleInfo>().ReverseMap();
            CreateMap<PagedList<RoleEntity>, PagedList<RoleInfo>>().ReverseMap();
            //CreateMap<RolePermissionEntity, RolePermissionInfo>().ReverseMap();
            //User
            CreateMap<UserEntity, UserInfo>().ReverseMap();
            CreateMap<PagedList<UserEntity>, PagedList<UserInfo>>().ReverseMap();
            //CreateMap<UserRoleEntity, UserRoleInfo>().ReverseMap();
            //MenuItem
            CreateMap<MenuItemEntity, MenuItemInfo>().ReverseMap();
            CreateMap<PagedList<MenuItemEntity>, PagedList<MenuItemInfo>>().ReverseMap();
        }
    }
}
