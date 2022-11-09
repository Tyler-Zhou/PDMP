using AutoMapper;
using PDMP.Contract;
using PDMP.Domain;
using PDMP.Infrastructure;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class ChapterService : IChapterService
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
        public ChapterService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<ChapterInfo> AddAsync(ChapterInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<ChapterEntity>(modelInfo);
            modelEntity.CreateDate = DateTime.Now;
            await _UnitOfWork.GetRepository<ChapterEntity>().InsertAsync(modelEntity);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<ChapterInfo>(modelEntity);
            throw new Exception("添加数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<ChapterEntity>();
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
        public async Task<IPagedList<ChapterInfo>> GetPagedListAsync(QueryParameter parameter)
        {
            var repository = _UnitOfWork.GetRepository<ChapterEntity>();
            var modelPagedList = await repository.GetPagedListAsync(predicate:
               x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
               pageIndex: parameter.PageIndex,
               pageSize: parameter.PageSize,
               orderBy: source => source.OrderByDescending(t => t.CreateDate));
            return _Mapper.Map<PagedList<ChapterInfo>>(modelPagedList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChapterInfo> GetSingleAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<ChapterEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return _Mapper.Map<ChapterInfo>(modelFind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<ChapterInfo> UpdateAsync(ChapterInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<ChapterEntity>(modelInfo);
            var repository = _UnitOfWork.GetRepository<ChapterEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(modelEntity.Id));

            modelFind.Content = modelEntity.Content;
            modelFind.Link = modelEntity.Link;
            modelFind.OrderIndex = modelEntity.OrderIndex;
            modelFind.IsError = modelEntity.IsError;
            modelFind.UpdateDate = DateTime.Now;

            repository.Update(modelFind);

            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<ChapterInfo>(modelFind);
            throw new Exception("更新数据异常:更新行数错误");
        }
        #endregion
    }
}
