﻿using Microsoft.AspNetCore.Mvc;
using PDMP.Contract.DTOs;
using PDMP.Contract.Interfaces;
using PDMP.Contract.Parameters;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _BookService;
        /// <summary>
        /// 书籍控制器
        /// </summary>
        /// <param name="bookService">书籍服务</param>
        public BookController(IBookService bookService)
        {
            _BookService = bookService;
        }

        /// <summary>
        /// 获取单本书籍
        /// </summary>
        /// <param name="id">书籍主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServerResponse> Get(Guid id) => await _BookService.GetSingleAsync(id);
        /// <summary>
        /// 获取所有书籍(分页)
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServerResponse> GetAll([FromQuery] BookParameter param) => await _BookService.GetAllAsync(param);
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="model">书籍实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Add([FromBody] BookDto model) => await _BookService.AddAsync(model);
        /// <summary>
        /// 更新书籍
        /// </summary>
        /// <param name="model">书籍实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Update([FromBody] BookDto model) => await _BookService.UpdateAsync(model);
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id">书籍主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ServerResponse> Delete(Guid id) => await _BookService.DeleteAsync(id);
    }
}
