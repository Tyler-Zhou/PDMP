using AutoMapper;
using MongoDB.Driver;
using PDMP.Contract.DTOs;
using PDMP.Contract.Extensions;
using PDMP.Contract.Interfaces;
using PDMP.Contract.Parameters;
using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;
using System.Security.Claims;
using System.Text;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserRepository _Repository;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _Repository = repository;
            _Mapper = mapper;
        }

        public Task<ServerResponse> AddAsync(UserDto dto)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ServerResponse> LoginAsync(string account, string password)
        {
            try
            {
                password = password.GetMD5();

                var filter = Builders<UserInfo>.Filter.And(Builders<UserInfo>.Filter.Eq("Account", account), Builders<UserInfo>.Filter.Eq("Password", password));
                var model = await _Repository.GetFirstOrDefaultAsync(filter: filter);

                if (model == null)
                    return new ServerResponse("账号或密码错误,请重试！");

                return new ServerResponse(true, new UserDto()
                {
                    Account = model.Account,
                    UserName = model.UserName,
                    Id = model.Id
                });
            }
            catch (Exception ex)
            {
                return new ServerResponse($"登录失败！异常:{ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> Resgiter(UserDto userDto)
        {
            try
            {
                var model = _Mapper.Map<UserInfo>(userDto);
                var filter = Builders<UserInfo>.Filter.Eq("Account", model.Account);
                var userModel = await _Repository.GetFirstOrDefaultAsync(filter);

                if (userModel != null)
                    return new ServerResponse($"当前账号:{model.Account}已存在,请重新注册！");

                model.CreateDate = DateTime.Now;
                model.Password = model.Password.GetMD5();

                if (await _Repository.InsertAsync(model))
                    return new ServerResponse(true, model);

                return new ServerResponse("注册失败,请稍后重试！");
            }
            catch (Exception ex)
            {
                return new ServerResponse($"注册账号失败！{ex.Message}");
            }
        }

        public Task<ServerResponse> UpdateAsync(UserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
