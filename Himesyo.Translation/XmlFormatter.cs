using System;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Xml.Serialization;

namespace Himesyo.Translation
{
    /// <summary>
    /// 可转换为 <see cref="IFormatter"/> 类型对象的 Xml 序列化器。
    /// </summary>
    public class XmlFormatter : XmlSerializer, IFormatter
    {
        /// <summary>
        /// 无效属性。
        /// </summary>
        ISurrogateSelector IFormatter.SurrogateSelector { get => default; set { } }
        /// <summary>
        /// 无效属性。
        /// </summary>
        SerializationBinder IFormatter.Binder { get => default; set { } }
        /// <summary>
        /// 无效属性。
        /// </summary>
        StreamingContext IFormatter.Context { get => default; set { } }
        /// <summary>
        /// 新实例初始化 <see cref="XmlFormatter"/> 类，它可以序列化的 XML 文档，将指定类型的对象反序列化 XML 文档化为指定类型的对象。
        /// </summary>
        /// <param name="type">对象的类型指示此 <see cref="XmlFormatter"/> 可进行序列化。</param>
        public XmlFormatter(Type type)
            : base(type) { }
        public XmlFormatter(Type type, XmlRootAttribute root)
            : base(type, root) { }
        public XmlFormatter(Type type, Type[] extraTypes)
            : base(type, extraTypes) { }
        public XmlFormatter(Type type, XmlAttributeOverrides overrides)
            : base(type, overrides) { }
        public XmlFormatter(Type type, string defaultNamespace)
            : base(type, defaultNamespace) { }
        public XmlFormatter(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace)
            : base(type, overrides, extraTypes, root, defaultNamespace) { }
        public XmlFormatter(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location)
            : base(type, overrides, extraTypes, root, defaultNamespace, location) { }
        [Obsolete]
        public XmlFormatter(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
            : base(type, overrides, extraTypes, root, defaultNamespace, location, evidence) { }
    }
}
