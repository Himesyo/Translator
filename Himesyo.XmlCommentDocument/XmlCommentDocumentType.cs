using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Himesyo.Translation;

namespace Himesyo.XmlCommentDocument
{
    [DisplayName("Xml 注释文档")]
    public class XmlCommentDocumentType : IFileType
    {
        public static FName TypeName { get; } = FName.Create<XmlCommentDocumentType>();
        public static IReadOnlyCollection<Filter> Filter { get; } = new ReadOnlyCollection<Filter>(new List<Filter>()
        {
            new Filter("*.xml", "Xml 注释文档"),
            new Filter("*.txt;*.vs*", "Xml 注释文档")
        });

        public FName Name => TypeName;
        public IReadOnlyCollection<Filter> OpenFileFilter => Filter;

        public bool TryCreate(string fileName, out IFile file, out string msg)
        {
            try
            {
                XDocument xml = XDocument.Load(fileName);
                if (xml.Root.Name == "doc")
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

        public bool TryOpen(string fileName, out IFile file, out string msg)
        {
            bool open = TryCreate(fileName, out file, out msg);
            return open;
        }

    }

    public class XmlCommentDocument : IFile
    {
        private XDocument xml;

        public FName TypeName => XmlCommentDocumentType.TypeName;
        public string Name { get; set; }
        public string FullPath { get; set; }
        public long FileSize { get; set; }
        public long TextLength { get; set; }
        public FileState State { get; set; }

        public XmlCommentDocument(XDocument document)
        {
            xml = document;
        }

        public bool TrySetState(FileState state)
        {
            State = state;
            return true;
        }

        public Task Init(ActionInfo actionInfo)
        {
            actionInfo.ResetResult();

            actionInfo.CompleteSuccess();
            return Task.CompletedTask;
        }

        public Task Start(ActionInfo actionInfo)
        {
            actionInfo.CompleteSuccess();
            return Task.CompletedTask;
        }

        public Task InitAsync(ActionInfo actionInfo, InitRunMessage run)
        {
            actionInfo.ResetResult();
            
            actionInfo.CompleteSuccess();
            return Task.CompletedTask;
        }

        public Task StartAsync(ActionInfo actionInfo, InitRunMessage run)
        {
            throw new NotImplementedException();
        }
    }
}
