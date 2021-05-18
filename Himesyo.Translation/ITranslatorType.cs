using System.IO;
using System.Runtime.Serialization;

namespace Himesyo.Translation
{
    public interface ITranslatorType
    {
        /// <summary>
        /// 类型的名称。
        /// </summary>
        TName Name { get; }
        /// <summary>
        /// 是否可以指定多个翻译器。
        /// </summary>
        bool Multiple { get; }
        /// <summary>
        /// 创建一个默认创建参数。
        /// </summary>
        /// <returns></returns>
        ICreateArgs CreateArgs();
        /// <summary>
        /// 获取参数编辑器。
        /// </summary>
        /// <returns></returns>
        ICreateArgsEditor GetCreateArgsEditor();
        /// <summary>
        /// 使用参数创建翻译器。
        /// </summary>
        /// <param name="createArgs"></param>
        /// <returns></returns>
        ITranslator CreateTranslator(ICreateArgs createArgs);
        /// <summary>
        /// 获取可对翻译器序列化和反序列化的序列化器。
        /// </summary>
        /// <returns></returns>
        IFormatter GetSerializer();
    }
}
