/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:34:48
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PDMP.Contract;
using PDMP.Domain;
using PDMP.Infrastructure;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        #region 服务(Service)
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _UnitOfWork;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        #endregion

        #region 公用方法(Public Method)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserInfo> AuthorizationAsync(string userName, string password)
        {
            var encryptPassword = password.GetMD5();

            if(await _UnitOfWork.GetRepository<UserEntity>().CountAsync()<=0)
            {
                await AddAsync(new UserInfo() {Name = userName,Password = password,Email="zhoubiyu@hotmail.com",PhoneNumber = "19138325961" });
            }
            var model = await _UnitOfWork.GetRepository<UserEntity>().GetFirstOrDefaultAsync(predicate:
                x => (x.Name.Equals(userName))
                && (x.Password.Equals(encryptPassword))
                );

            if (model == null)
                throw new Exception("账号或密码错误,请重试！");
            return _Mapper.Map<UserInfo>(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public async Task<bool> CheckPermissionAsync(long userId, string permissionName)
        {
            var userRoleRepository = _UnitOfWork.GetRepository<UserRoleEntity>();
            var allModel =await userRoleRepository.GetAllAsync(
                predicate: x => x.User.Id.Equals(userId),
                include:source=>source.Include(t=>t.Role.RolePermissions));
            if (allModel == null) 
                return false;
            foreach (var item in allModel)
            {
                return item.Role.RolePermissions.Any(p => permissionName.StartsWith(p.Permission.Name));
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<UserInfo> GetSingleAsync(string userName)
        {
            var repository = _UnitOfWork.GetRepository<UserEntity>();
            var firstOrDefaultModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Name.Equals(userName));
            return _Mapper.Map<UserInfo>(firstOrDefaultModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<UserInfo> AddAsync(UserInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<UserEntity>(modelInfo);
            modelEntity.CreateDate = DateTime.Now;
            await _UnitOfWork.GetRepository<UserEntity>().InsertAsync(modelEntity);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<UserInfo>(modelInfo);
            throw new Exception("添加数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<UserEntity>();
            var firstOrDefaultModel = await repository
                .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            repository.Delete(firstOrDefaultModel);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return true;
            throw new Exception("删除数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<IPagedList<UserInfo>> GetPagedListAsync(QueryParameter parameter)
        {
            var repository = _UnitOfWork.GetRepository<UserEntity>();
            var pageListModel = await repository.GetPagedListAsync(predicate:
               x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
               pageIndex: parameter.PageIndex,
               pageSize: parameter.PageSize,
               orderBy: source => source.OrderByDescending(t => t.CreateDate));
            return _Mapper.Map<PagedList<UserInfo>>(pageListModel);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserInfo> GetSingleAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<UserEntity>();
            var firstOrDefaultModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return _Mapper.Map<UserInfo>(firstOrDefaultModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<UserInfo> UpdateAsync(UserInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<UserEntity>(modelInfo);
            var repository = _UnitOfWork.GetRepository<UserEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(modelEntity.Id));

            modelFind.Email = modelEntity.Email;
            modelFind.PhoneNumber = modelEntity.PhoneNumber;
            modelFind.RefreshToken = modelEntity.RefreshToken;
            modelFind.RefreshTokenExpiryTime = modelEntity.RefreshTokenExpiryTime;
            modelFind.UpdateDate = DateTime.Now;

            repository.Update(modelFind);

            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<UserInfo>(modelFind);
            throw new Exception("更新数据失败:更新行数错误");
        }
        #endregion
    }
}

