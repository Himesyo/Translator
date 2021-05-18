﻿using System;
using System.ComponentModel;

using Himesyo.Runtime;

namespace Himesyo.Translation
{
    /// <summary>
    /// 表示  <see cref="IFileType"/> 的名称。
    /// </summary>
    public class FName
    {
        /// <summary>
        /// 从指定类型创建名称。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static FName Create<T>()
            where T : class, IFileType
        {
            Type type = typeof(T);
            string name = type.GetDisplayName();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = type.Name;
            }

            FName fname = new FName()
            {
                Name = name,
                FileType = type
            };
            return fname;
        }

        /// <summary>
        /// 名称或友好名称。它从 <see cref="IFileType"/> 的实现类的 <see cref="DisplayNameAttribute"/> 特性获取。
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 此名称指示的 <see cref="IFileType"/> 类型。
        /// </summary>
        public Type FileType { get; private set; }

        private FName()
        {

        }

        public override bool Equals(object obj)
        {
            if (obj is FName name)
            {
                return this == name;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return FileType.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
        public static bool operator ==(FName left, FName right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.FileType == right.FileType;
        }
        public static bool operator !=(FName left, FName right)
        {
            if (left is null && right is null) return false;
            if (left is null || right is null) return true;
            return left.FileType != right.FileType;
        }
    }
}
