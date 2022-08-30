using PDMP.Contract.DTOs;
using PDMP.Contract.Interfaces;
using PDMP.Contract.Parameters;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionService : IVersionService
    {
        public Task<ServerResponse> AddAsync(VersionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServerResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServerResponse> GetAllAsync(QueryParameter query)
        {
            throw new NotImplementedException();
        }

        public Task<ServerResponse> GetSingleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServerResponse> UpdateAsync(VersionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
