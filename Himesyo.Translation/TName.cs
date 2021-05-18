using System;
using System.ComponentModel;

using Himesyo.Runtime;

namespace Himesyo.Translation
{
    /// <summary>
    /// 表示 <see cref="ITranslatorType"/> 的名称。
    /// </summary>
    public class TName
    {
        /// <summary>
        /// 从指定类型创建名称。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static TName Create<T>()
            where T : class, ITranslatorType
        {
            Type type = typeof(T);
            string name = type.GetDisplayName();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = type.Name;
            }

            TName tname = new TName()
            {
                Name = name,
                TranslatorType = type
            };
            return tname;
        }

        /// <summary>
        /// 名称或友好名称。它从 <see cref="ITranslatorType"/> 的实现类的 <see cref="DisplayNameAttribute"/> 特性获取。
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 此名称指示的 <see cref="ITranslatorType"/> 类型。
        /// </summary>
        public Type TranslatorType { get; private set; }

        private TName()
        {

        }

        public override bool Equals(object obj)
        {
            if (obj is TName name)
            {
                return this == name;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return TranslatorType.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
        public static bool operator ==(TName left, TName right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.TranslatorType == right.TranslatorType;
        }
        public static bool operator !=(TName left, TName right)
        {
            if (left is null && right is null) return false;
            if (left is null || right is null) return true;
            return left.TranslatorType != right.TranslatorType;
        }
    }
}
