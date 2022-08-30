﻿using PDMP.Client.Core.Models;

namespace PDMP.Library.Client.Models
{
    /// <summary>
    /// 章节查询参数
    /// </summary>
    public class ChapterParameter : QueryParameter
    {
        /// <summary>
        /// 书籍唯一键
        /// </summary>
        public string BookKey { get; set; }
    }
}
