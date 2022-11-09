/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:34:48
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using AutoMapper;
using PDMP.Contract;
using PDMP.Domain;
using PDMP.Infrastructure;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleService : IRoleService
    {
        #region 成员(Member)
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
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        #endregion

        #region 公用方法(Public Method)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<RoleInfo> AddAsync(RoleInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<RoleEntity>(modelInfo);
            modelEntity.CreateDate = DateTime.Now;
            await _UnitOfWork.GetRepository<RoleEntity>().InsertAsync(modelEntity);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<RoleInfo>(modelEntity);
            throw new Exception("添加数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<RoleEntity>();
            var modelFind = await repository
                .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            repository.Delete(modelFind);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return true;
            throw new Exception("删除数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<IPagedList<RoleInfo>> GetPagedListAsync(QueryParameter parameter)
        {
            var repository = _UnitOfWork.GetRepository<RoleEntity>();
            var modelPagedList = await repository.GetPagedListAsync(predicate:
               x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
               pageIndex: parameter.PageIndex,
               pageSize: parameter.PageSize,
               orderBy: source => source.OrderByDescending(t => t.CreateDate));
            return _Mapper.Map<PagedList<RoleInfo>>(modelPagedList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RoleInfo> GetSingleAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<RoleEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return _Mapper.Map<RoleInfo>(modelFind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<RoleInfo> UpdateAsync(RoleInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<RoleEntity>(modelInfo);
            var repository = _UnitOfWork.GetRepository<RoleEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(modelEntity.Id));

            modelFind.Name = modelEntity.Name;
            modelFind.UpdateDate = DateTime.Now;

            repository.Update(modelFind);

            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<RoleInfo>(modelFind);
            throw new Exception("更新数据异常:更新行数错误");
        }
        #endregion
    }
}

