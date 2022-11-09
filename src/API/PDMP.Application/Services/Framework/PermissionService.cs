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
    public class PermissionService : IPermissionService
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
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<PermissionInfo> AddAsync(PermissionInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<PermissionEntity>(modelInfo);
            modelEntity.CreateDate = DateTime.Now;
            await _UnitOfWork.GetRepository<PermissionEntity>().InsertAsync(modelEntity);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<PermissionInfo>(modelEntity);
            throw new Exception("添加数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<PermissionEntity>();
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
        public async Task<IPagedList<PermissionInfo>> GetPagedListAsync(QueryParameter parameter)
        {
            var repository = _UnitOfWork.GetRepository<PermissionEntity>();
            var modelPagedList = await repository.GetPagedListAsync(predicate:
               x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
               pageIndex: parameter.PageIndex,
               pageSize: parameter.PageSize,
               orderBy: source => source.OrderByDescending(t => t.CreateDate));
            return _Mapper.Map<PagedList<PermissionInfo>>(modelPagedList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PermissionInfo> GetSingleAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<PermissionEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return _Mapper.Map<PermissionInfo>(modelFind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PermissionInfo> UpdateAsync(PermissionInfo model)
        {
            var dbModel = _Mapper.Map<PermissionEntity>(model);
            var repository = _UnitOfWork.GetRepository<PermissionEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbModel.Id));

            modelFind.Name = dbModel.Name;
            modelFind.UpdateDate = DateTime.Now;

            repository.Update(modelFind);

            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<PermissionInfo>(modelFind);
            throw new Exception("更新数据异常:更新行数错误");
        }
        #endregion
    }
}

