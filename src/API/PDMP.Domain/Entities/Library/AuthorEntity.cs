/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:42:17
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 作者实体
    /// </summary>
    public class AuthorEntity:BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 书籍集
        /// </summary>
        public virtual List<BookEntity> Books { get; set; }
        #endregion

        #region 服务(Service)
        #endregion

        #region 命令(Command)
        #endregion

        #region 构造函数(Constructor)
        #endregion

        #region 事件(Event)
        #endregion

        #region 公用方法(Public Method)
        #endregion

        #region 私有方法(Private Method)
        #endregion

    }
}

