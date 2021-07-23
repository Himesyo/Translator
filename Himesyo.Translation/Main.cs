using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Himesyo.Translation
{
    /// <summary>
    /// 表示一个可翻译的文件类型。
    /// </summary>
    public interface IFileType
    {
        /// <summary>
        /// 文件类型的名称。
        /// </summary>
        FName Name { get; }
        /// <summary>
        /// 可打开的文件筛选器。
        /// </summary>
        IReadOnlyCollection<Filter> OpenFileFilter { get; }
        /// <summary>
        /// 尝试打开指定的文件。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool TryOpen(string fileName, out IFile file, out string msg);
    }
    /// <summary>
    /// 表示一个可翻译的文件。
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// 与 <see cref="IFile"/> 关联的 <see cref="IFileType"/> 的名称。
        /// </summary>
        FName TypeName { get; }
        /// <summary>
        /// 文件的显示名称。
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 文件的完整路径。
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// 文件所占的磁盘大小。
        /// </summary>
        long FileSize { get; }
        /// <summary>
        /// 需要翻译的文本长度。
        /// </summary>
        long TextLength { get; }
        /// <summary>
        /// 当前状态。
        /// </summary>
        string State { get; }
        /// <summary>
        /// 尝试将此文件的状态设置为指定值。
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool TrySetState(string state);

        Task Init(CancellationToken cancellationToken);
        Task Start(CancellationToken cancellationToken);
    }
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
