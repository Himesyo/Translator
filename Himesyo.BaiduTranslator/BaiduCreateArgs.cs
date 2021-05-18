using System;
using System.ComponentModel;

using Himesyo.Translation;

namespace Himesyo.BaiduTranslator
{
    [Serializable]
    public class BaiduCreateArgs : ICreateArgs
    {
        [Browsable(false)]
        public TName TypeName => BaiduTranslatorType.TypeName;

        [Category("百度翻译")]
        [DisplayName("AppID")]
        [Description("百度提供的AppID")]
        public string AppID { get; set; }
        [Category("百度翻译")]
        [DisplayName("密钥")]
        [Description("百度提供的密钥")]
        [PasswordPropertyText(true)]
        public string SecretKey { get; set; }

        [Category("通用")]
        [DisplayName("翻译间隔")]
        [Description("如果是标准版建议大于500.")]
        public int Interval { get; set; } = 1100;
    }

    public class ReadOnlyBaiduCreateArgs : ICreateArgs
    {
        [Browsable(false)]
        public TName TypeName => BaiduTranslatorType.TypeName;

        private BaiduTranslator translator;

        [Category("百度翻译")]
        [DisplayName("AppID")]
        [Description("百度提供的AppID。无法修改，请重新创建。")]
        public string AppID => translator.AppID;
        [Category("百度翻译")]
        [DisplayName("密钥")]
        [Description("百度提供的密钥。无法修改，请重新创建。")]
        [PasswordPropertyText(true)]
        public string SecretKey => translator.SecretKey;

        [Category("通用")]
        [DisplayName("翻译间隔")]
        [Description("如果是标准版建议大于500.")]
        public int Interval
        {
            get => translator.Interval;
            set => translator.Interval = value;
        }

        public ReadOnlyBaiduCreateArgs(BaiduTranslator translator)
        {
            this.translator = translator;
        }
    }
}
