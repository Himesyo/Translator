using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using Himesyo.Check;
using Himesyo.IO;
using Himesyo.Translation;

using Newtonsoft.Json.Linq;

namespace Himesyo.BaiduTranslator
{
    [Serializable]
    public sealed class BaiduTranslator : ITranslator
    {
        [XmlIgnore]
        private static Uri UriHttp { get; }
          = new Uri(@"http://api.fanyi.baidu.com/api/trans/vip/translate");

        [XmlIgnore]
        private static Uri UriHttps { get; }
          = new Uri(@"https://fanyi-api.baidu.com/api/trans/vip/translate");

        [NonSerialized]
        private readonly Random random = new Random();

        [XmlIgnore]
        public TName TypeName => BaiduTranslatorType.TypeName;
        [XmlIgnore]
        public string FileName => $"{AppID}.baidu";

        public int Interval { get; set; }

        public string AppID { get; set; }
        public string SecretKey { get; set; }
        [XmlIgnore]
        public Language SourceLanguage { get; set; } = Language.auto;
        [XmlIgnore]
        public Language TargetLanguage { get; set; } = Language.zh;
        [XmlIgnore]
        public BaiduCreateArgs CreateArgs { get; }
        [XmlIgnore]
        ICreateArgs ITranslator.CreateArgs => this.ToShow();

        public BaiduTranslator()
        {
            CreateArgs = new BaiduCreateArgs();
        }
        public BaiduTranslator(BaiduCreateArgs args)
        {
            CreateArgs = args;
            AppID = args.AppID;
            SecretKey = args.SecretKey;
            Interval = args.Interval;
        }

        public void ResetLanguage()
        {
            SourceLanguage = Language.auto;
            TargetLanguage = Language.zh;
        }
        public bool CanTranslate(Language sourceLanguage, Language targetLanguage)
        {
            return true;
        }
        public string Translate(string text)
        {
            return Translate(text, SourceLanguage, TargetLanguage);
        }
        public string Translate(string text, Language sourceLanguage, Language targetLanguage)
        {
            SourceLanguage = sourceLanguage;
            TargetLanguage = targetLanguage;
            if (string.IsNullOrWhiteSpace(text))
                return "";

            string requestResult;
            string salt = random.Next().ToString();

            string get = string.Format(
            "?q={0}&from={1}&to={2}&appid={3}&salt={4}&sign={5}"
            , Uri.EscapeDataString(text)
            , sourceLanguage.ToString()
            , targetLanguage.ToString()
            , AppID
            , salt
            , $"{AppID}{text}{salt}{SecretKey}".ComputeMD5().ToShow());

            try
            {
                HttpWebRequest web = (HttpWebRequest)WebRequest.Create(UriHttp + get);
                using (WebResponse response = web.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader sr = new StreamReader(responseStream, Encoding.UTF8))
                {
                    requestResult = sr.ReadToEnd();
                }
                JObject json = JObject.Parse(requestResult);
                if (Check(json, out string src, out string dst))
                {
                    return dst;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private bool Check(JObject json, out string src, out string dst)
        {
            src = "";
            dst = "";
            if (json.TryGetValue("error_code", out JToken value))
            {
                return false;
            }
            else
            {
                JToken token = json["trans_result"][0];
                src = token["src"].Value<string>();
                dst = token["dst"].Value<string>();
                return true;
            }
        }
        private ReadOnlyBaiduCreateArgs ToShow()
        {
            return new ReadOnlyBaiduCreateArgs(this);
        }

        public override string ToString()
        {
            return $"百度翻译 - {AppID}";
        }

        public override bool Equals(object obj)
        {
            if (obj is BaiduTranslator translator)
            {
                return this == translator;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return AppID?.GetHashCode() ?? 0;
        }
        public static bool operator ==(BaiduTranslator left, BaiduTranslator right)
        {
            return left?.AppID == right?.AppID;
        }
        public static bool operator !=(BaiduTranslator left, BaiduTranslator right)
        {
            return left?.AppID != right?.AppID;
        }
    }
}
