/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-10 0:02:07
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using System.Security.Cryptography;
using System.Text;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        #region 扩展方法(Extension Method)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetMD5(this string data)
        {

            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data));

            var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(data));
            //将加密后的字节数组转换为加密字符串
            return Convert.ToBase64String(hash);
        }
        #endregion
    }
}

