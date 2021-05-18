using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Xml.Serialization;

using Himesyo.Translation;

namespace Himesyo.BaiduTranslator
{
    [DisplayName("百度翻译")]
    public class BaiduTranslatorType : ITranslatorType
    {
        static BaiduTranslatorType()
        {
            BaiduCreateArgs args = new BaiduCreateArgs();
            args.Interval = 1000;
        }

        public static TName TypeName { get; } = TName.Create<BaiduTranslatorType>();

        public TName Name => TypeName;

        public bool Multiple { get; } = true;

        public ICreateArgs CreateArgs()
        {
            return new BaiduCreateArgs();
        }

        public ITranslator CreateTranslator(ICreateArgs createArgs)
        {
            if (createArgs is BaiduCreateArgs args)
            {
                return new BaiduTranslator(args);
            }
            else if (createArgs is ReadOnlyBaiduCreateArgs readOnlyArgs)
            {
                return new BaiduTranslator(new BaiduCreateArgs()
                {
                    AppID = readOnlyArgs.AppID,
                    SecretKey = readOnlyArgs.SecretKey,
                    Interval = readOnlyArgs.Interval
                });
            }
            return null;
        }

        public ICreateArgsEditor GetCreateArgsEditor()
        {
            return new BaiduCreateArgsEditor();
        }

        public IFormatter GetSerializer()
        {
            return new XmlFormatter(typeof(BaiduTranslator));
            //return new BinaryFormatter();
        }
    }

}
