using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
        /// 使用指定文件尝试创建一个 <see cref="IFile"/> 文件。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool TryCreate(string fileName, out IFile file, out string msg);
        /// <summary>
        /// 尝试打开已经翻译一部分的缓存文件。
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
        FileState State { get; }
        /// <summary>
        /// 尝试将此文件的状态设置为指定值。
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool TrySetState(FileState state);

        /// <summary>
        /// 创建界面后进行的初始化操作。例如分析文件内容，计算文本长度，分析翻译进度等。
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <param name="run"></param>
        /// <returns></returns>
        Task InitAsync(ActionInfo actionInfo, InitRunMessage run);
        /// <summary>
        /// 开始翻译操作。
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <param name="run"></param>
        /// <returns></returns>
        Task StartAsync(ActionInfo actionInfo, InitRunMessage run);
    }

    /// <summary>
    /// 初始化过程中发送消息的对象。
    /// </summary>
    public class InitRunMessage : ActionRunMessage
    {
        /// <summary>
        /// 刷新基本信息。例如文本长度等。
        /// </summary>
        public void RefreshInfo()
        {

        }

    }
}
