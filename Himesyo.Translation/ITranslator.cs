using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Himesyo.Translation
{
    public interface ITranslator
    {
        /// <summary>
        /// 与此对象关联的 <see cref="ITranslatorType"/> 类型的名称。
        /// </summary>
        TName TypeName { get; }
        /// <summary>
        /// 保存到文件时使用的文件名。扩展名自动添加。
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// 获取创建时使用的参数。
        /// </summary>
        ICreateArgs CreateArgs { get; }
        /// <summary>
        /// 两次翻译之间需要停顿的时间间隔。
        /// </summary>
        int Interval { get; }
        /// <summary>
        /// 重置记录的语言。
        /// </summary>
        void ResetLanguage();
        /// <summary>
        /// 此翻译器是否支持指定语言的翻译。
        /// </summary>
        /// <param name="sourceLanguage"></param>
        /// <param name="targetLanguage"></param>
        /// <returns></returns>
        bool CanTranslate(Language sourceLanguage, Language targetLanguage);
        /// <summary>
        /// 使用指定语言翻译指定文本。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sourceLanguage"></param>
        /// <param name="targetLanguage"></param>
        /// <returns></returns>
        string Translate(string text, Language sourceLanguage, Language targetLanguage);
        /// <summary>
        /// 翻译指定文本。使用上次使用的语言。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sourceLanguage"></param>
        /// <param name="targetLanguage"></param>
        /// <returns></returns>
        string Translate(string text);
    }
}
