namespace PDMP.Client.Core
{
    /// <summary>
    /// Object 扩展
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 强转为某个类型
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A T.</returns>
        public static T AssertCast<T>(this object value)
        {
            return (T)((object)value);
        }

        /// <summary>
        /// 强转为某个类型, 不为空
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A T.</returns>
        public static T AssertCastNotNull<T>(this object value)
        {
            return value.AssertCast<T>();
        }
    }
}