using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using Himesyo.Translation;

namespace Himesyo.XmlCommentDocument
{
    [DisplayName("Xml 注释文档")]
    public class XmlCommentDocumentType : IFileType
    {
        public static FName TypeName { get; } = FName.Create<XmlCommentDocumentType>();
        public static IReadOnlyCollection<Filter> Filter = new ReadOnlyCollection<Filter>(new List<Filter>()
        {
            new Filter("*.xml", "Xml 注释文档"),
            new Filter("*.txt;*.vs*", "Xml 注释文档")
        });

        public FName Name => TypeName;
        public IReadOnlyCollection<Filter> OpenFileFilter => Filter;

        public bool TryOpen(string fileName, out IFile file, out string msg)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(fileName);
                if (xml.DocumentElement.LocalName == "doc")
                {
                    XmlCommentDocument result = new XmlCommentDocument(xml);
                    file = result;
                    msg = "";
                    return true;
                }
                else
                {
                    file = null;
                    msg = "文件格式不正确：无法识别的根元素类型。";
                    return false;
                }
            }
            catch (Exception ex)
            {
                file = null;
                msg = $"无法读取文件：{ex.Message}";
                return false;
            }
        }
    }

    public class XmlCommentDocument : IFile
    {
        public FName TypeName => XmlCommentDocumentType.TypeName;
        public string Name { get; set; }
        public string FullPath { get; set; }
        public long FileSize { get; set; }
        public long TextLength { get; set; }
        public string State { get; }

        public XmlCommentDocument(XmlDocument document)
        {

        }

        public bool TrySetState(string state)
        {
            throw new NotImplementedException();
        }

        public Task Init(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Start(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
