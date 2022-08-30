using PDMP.Client.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMP.Library.Client.Models
{
    /// <summary>
    /// 书籍查询参数
    /// </summary>
    public class BookParameter : QueryParameter
    {
        /// <summary>
        /// 书籍状态
        /// </summary>
        public int? Status { get; set; }
    }
}
