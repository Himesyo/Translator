using System.ComponentModel;

namespace Himesyo.Translation
{
    public interface ICreateArgs
    {
        /// <summary>
        /// 与此对象关联的 <see cref="ITranslatorType"/> 类型的名称。
        /// </summary>
        [Browsable(false)]
        TName TypeName { get; }

        /// <summary>
        /// 调用后距离下次调用需要等待的时间。
        /// </summary>
        [Category("通用")]
        [DisplayName("翻译间隔")]
        [Description("调用后距离下次调用需要等待的时间。")]
        int Interval { get; }
    }
}
