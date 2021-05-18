using System.Windows.Forms;

namespace Himesyo.Translation
{
    public interface ICreateArgsEditor
    {
        /// <summary>
        /// 与此对象关联的 <see cref="ITranslatorType"/> 类型的名称。
        /// </summary>
        TName TypeName { get; }

        /// <summary>
        /// 获取或设置正在编辑的参数。
        /// </summary>
        ICreateArgs EditArgs { get; set; }
        /// <summary>
        /// 用于编辑参数的编辑器。
        /// </summary>
        Control ShowControl { get; }
    }
}
