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
    public class BookService : IBookService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBookRepository _BookRepository;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="mapper"></param>
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _BookRepository = bookRepository;
            _Mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> AddAsync(BookDto dto)
        {
            try
            {
                var updateModel = _Mapper.Map<BookInfo>(dto);
                updateModel.CreateDate = DateTime.Now;
                if (await _BookRepository.InsertAsync(updateModel))
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
                if (await _BookRepository.DeleteAsync(id))
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
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> UpdateAsync(BookDto dto)
        {
            try
            {
                var model = _Mapper.Map<BookInfo>(dto);
                var filter = Builders<BookInfo>.Filter.Empty;
                if (!Guid.Empty.Equals(model.Id))
                {
                    filter = Builders<BookInfo>.Filter.Eq("_id", model.Id);
                }
                var updateModel = await _BookRepository.GetFirstOrDefaultAsync(filter);
                if (updateModel == null)
                {
                    updateModel = new BookInfo();
                }
                updateModel.Key = model.Key;
                updateModel.Name = model.Name;
                updateModel.Author = model.Author;
                updateModel.Link = model.Link;
                updateModel.Tag = model.Tag;
                updateModel.Introduction = model.Introduction;
                updateModel.Status = model.Status;
                updateModel.UpdateDate = DateTime.Now;

                if (await _BookRepository.UpdateAsync(updateModel))
                    return new ServerResponse(true, updateModel);
                return new ServerResponse("更新数据异常！");
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
                var filter = Builders<BookInfo>.Filter.In("Name", parameter.Search);
                var sort = Builders<BookInfo>.Sort.Descending("CreateDate");


                var models = await _BookRepository.GetPagedListAsync(
                    filter: filter,
                    sort: sort,
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize);
                return new ServerResponse(true, models);
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
        public async Task<ServerResponse> GetAllAsync(BookParameter parameter)
        {
            try
            {
                var filter = Builders<BookInfo>.Filter.Empty;
                if (!string.IsNullOrWhiteSpace(parameter.Search) && parameter.Status != null)
                {
                    filter = Builders<BookInfo>.Filter.And(Builders<BookInfo>.Filter.In("Name", parameter.Search), Builders<BookInfo>.Filter.Eq("Status", parameter.Status));
                }
                else if (!string.IsNullOrWhiteSpace(parameter.Search))
                {
                    filter = Builders<BookInfo>.Filter.In("Name", parameter.Search);
                }
                else if (parameter.Status != null)
                {
                    filter = Builders<BookInfo>.Filter.Eq("Status", parameter.Status);
                }
                var sort = Builders<BookInfo>.Sort.Descending("tCreateDate");

                var models = await _BookRepository.GetPagedListAsync(filter: filter,
                    sort: sort,
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize);
                return new ServerResponse(true, models);
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
                var filter = Builders<BookInfo>.Filter.Eq("Id", id);
                var sort = Builders<BookInfo>.Sort.Descending("CreateDate");
                var updateModel = await _BookRepository.GetFirstOrDefaultAsync(filter, sort);
                return new ServerResponse(true, updateModel);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
    }
}
