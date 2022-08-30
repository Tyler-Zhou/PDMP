﻿namespace PDMP.Contract.DTOs
{
    /// <summary>
    /// 服务器响应对象
    /// </summary>
    public class ServerResponse
    {
        /// <summary>
        /// 服务器响应对象(提示错误/异常调用)
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="status">状态</param>
        public ServerResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }
        /// <summary>
        /// 服务器响应对象(存在数据调用)
        /// </summary>
        /// <param name="status">消息</param>
        /// <param name="result">结果对象</param>
        /// <param name="token">令牌</param>
        public ServerResponse(bool status, object result, string token = "")
        {
            Status = status;
            Result = result;
            Token = token;
        }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 结果对象
        /// </summary>
        public object Result { get; set; }
    }
}
