using AutoMapper;
using PDMP.Contract.DTOs;
using PDMP.Domain.Entities;

namespace PDMP.Api.Mapper
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
            CreateMap<UserInfo, UserDto>().ReverseMap();
            CreateMap<VersionInfo, VersionDto>().ReverseMap();
            CreateMap<BookInfo, BookDto>().ReverseMap();
            CreateMap<ChapterInfo, ChapterDto>().ReverseMap();
        }
    }
}
