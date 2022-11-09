using AutoMapper;
using PDMP.Contract;
using PDMP.Domain;
using PDMP.Infrastructure;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class BookService : IBookService
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
        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<BookInfo> AddAsync(BookInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<BookEntity>(modelInfo);
            modelEntity.CreateDate = DateTime.Now;
            await _UnitOfWork.GetRepository<BookEntity>().InsertAsync(modelEntity);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<BookInfo>(modelEntity);
            throw new Exception("添加数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<BookEntity>();
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
        public async Task<IPagedList<BookInfo>> GetPagedListAsync(QueryParameter parameter)
        {
            var repository = _UnitOfWork.GetRepository<BookEntity>();
            var modelPagedList = await repository.GetPagedListAsync(predicate:
               x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
               pageIndex: parameter.PageIndex,
               pageSize: parameter.PageSize,
               orderBy: source => source.OrderByDescending(t => t.CreateDate));
            return _Mapper.Map<PagedList<BookInfo>>(modelPagedList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BookInfo> GetSingleAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<BookEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return _Mapper.Map<BookInfo>(modelFind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<BookInfo> UpdateAsync(BookInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<BookEntity>(modelInfo);
            var repository = _UnitOfWork.GetRepository<BookEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(modelEntity.Id));

            modelFind.Link = modelEntity.Link;
            modelFind.AuthorId = modelEntity.AuthorId;
            modelFind.Tag = modelEntity.Tag;
            modelFind.Introduction = modelEntity.Introduction;
            modelFind.PosterLink = modelEntity.PosterLink;
            modelFind.PosterContent = modelEntity.PosterContent;
            modelFind.Status = modelEntity.Status;
            modelFind.UpdateDate = DateTime.Now;

            repository.Update(modelFind);

            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<BookInfo>(modelFind);
            throw new Exception("更新数据异常:更新行数错误");
        }
        #endregion
    }
}
