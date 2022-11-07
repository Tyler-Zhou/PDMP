using AutoMapper;
using MongoDB.Driver;
using PDMP.Contract.DTOs;
using PDMP.Contract.Interfaces;
using PDMP.Contract.Parameters;
using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class ChapterService : IChapterService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IChapterRepository _ChapterRepository;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chapterRepository"></param>
        /// <param name="mapper"></param>
        public ChapterService(IChapterRepository chapterRepository, IMapper mapper)
        {
            _ChapterRepository = chapterRepository;
            _Mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> AddAsync(ChapterDto dto)
        {
            try
            {
                var updateModel = _Mapper.Map<ChapterInfo>(dto);
                updateModel.CreateDate = DateTime.Now;
                _ChapterRepository.CollectionName = updateModel.BookKey;
                if (await _ChapterRepository.InsertAsync(updateModel))
                    return new ServerResponse(true, updateModel);
                return new ServerResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServerResponse> DeleteAsync(Guid id)
        {
            try
            {
                if (await _ChapterRepository.DeleteAsync(id))
                    return new ServerResponse(true, "");
                return new ServerResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<ServerResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var projection = Builders<ChapterInfo>.Projection.Include("Id").Include("BookKey").Include("Name").Include("Link").Include("CreateDate").Include("UpdateDate");
                var filter = Builders<ChapterInfo>.Filter.In("Name", parameter.Search);
                var sort = Builders<ChapterInfo>.Sort.Descending("CreateDate");


                var todos = await _ChapterRepository.GetPagedListAsync(
                    projection: projection,
                    filter: filter,
                    sort: sort,
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize
                   );
                return new ServerResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<ServerResponse> GetAllAsync(ChapterParameter parameter)
        {
            try
            {
                var projection = Builders<ChapterInfo>.Projection.Include("Id").Include("BookKey").Include("Name").Include("Link").Include("CreateDate").Include("UpdateDate");
                var filter = Builders<ChapterInfo>.Filter.In("Name", parameter.Search);
                var sort = Builders<ChapterInfo>.Sort.Descending("CreateDate");

                var todos = await _ChapterRepository.GetPagedListAsync(
                    projection: projection,
                    filter: filter,
                    sort: sort,
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize
                   );
                return new ServerResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServerResponse> GetSingleAsync(Guid id)
        {
            try
            {
                var filter = Builders<ChapterInfo>.Filter.Eq("Id", id);
                var todo = await _ChapterRepository.GetFirstOrDefaultAsync(filter);
                return new ServerResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServerResponse> UpdateAsync(ChapterDto model)
        {
            try
            {
                var dbToDo = _Mapper.Map<ChapterInfo>(model);
                var filter = Builders<ChapterInfo>.Filter.Eq("Id", dbToDo.Id);
                _ChapterRepository.CollectionName = model.BookKey;
                var todo = await _ChapterRepository.GetFirstOrDefaultAsync(filter);

                todo.Key = dbToDo.Key;
                todo.Name = dbToDo.Name;
                todo.Content = dbToDo.Content;
                todo.Link = dbToDo.Link;
                todo.UpdateDate = DateTime.Now;

                if (await _ChapterRepository.UpdateAsync(todo))
                    return new ServerResponse(true, todo);
                return new ServerResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
    }
}
