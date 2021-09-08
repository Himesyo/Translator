namespace Himesyo.Translation
{
    /// <summary>
    /// 表示一个文件筛选器。
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// 扩展名列表。可使用问号(?)和星号(*)通配符。多个扩展名使用分号(;)分割。
        /// </summary>
        public string Extension { get; protected set; }
        /// <summary>
        /// 筛选器标题。
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// 默认构造函数。
        /// </summary>
        protected Filter()
        {

        }
        /// <summary>
        /// 使用指定的扩展名和标题初始化新实例。
        /// </summary>
        /// <param name="extension">扩展名列表。可使用问号(?)和星号(*)通配符。多个扩展名使用分号(;)分割。</param>
        /// <param name="title">筛选器标题。</param>
        public Filter(string extension, string title)
        {
            Extension = extension;
            Title = title;
        }
    }
}
