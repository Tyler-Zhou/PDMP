namespace PDMP.Contract.DTOs
{
    /// <summary>
    /// 文件传输对象
    /// </summary>
    public class FileDto
    {
        /// <summary>
        /// 读取起始(位置)
        /// </summary>
        public long From { get; set; }
        /// <summary>
        /// 读取至(位置)
        /// </summary>
        public long To { get; set; }
        /// <summary>
        /// 是否部分文件
        /// </summary>
        public bool IsPartial { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public long Length { get; set; }
    }
}
