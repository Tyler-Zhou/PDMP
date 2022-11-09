/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:25:11
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 服务器响应对象
    /// </summary>
    public class ApiResponse
    {
        #region 成员(Member)
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 结果对象
        /// </summary>
        public object Result { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 服务器响应对象(提示错误/异常调用)
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="status">状态</param>
        public ApiResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }
        /// <summary>
        /// 服务器响应对象(存在数据调用)
        /// </summary>
        /// <param name="status">消息</param>
        /// <param name="result">结果对象</param>
        public ApiResponse(bool status, object result)
        {
            Status = status;
            Result = result;
        }
        #endregion
    }
}

