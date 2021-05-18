using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.Translation;

namespace Himesyo.DocumentTranslator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Environment.CurrentDirectory = Application.StartupPath;

            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var select = from assembly in assemblies
                         where assembly.FullName == args.Name
                         select assembly;
            var ass = select.FirstOrDefault();
            if (ass != null)
            {
                return ass;
            }
            return null;
        }
    }

    internal class TestFile : IFile
    {
        public FName TypeName { get; }
        public string Name => "测试";
        public string FullPath => @"Himesyo\DocumentTranslator\测试.xml";
        public long FileSize => 3456L;
        public long TextLength => 1234L;
    }

    public class Document
    {
        [Browsable(false)]
        public IFile File { get; set; }
        [DisplayName("名称")]
        public string Name
        {
            get
            {
                return File.Name;
            }
            set
            {

            }
        }

        [DisplayName("完整路径")]
        public string FullPath
        {
            get
            {
                return File.FullPath;
            }
            set
            {

            }
        }

        [DisplayName("文件大小")]
        [Description("文件的物理大小。单位字节。")]
        public long FileSize
        {
            get
            {
                return File.FileSize;
            }
            set
            {

            }
        }

        [DisplayName("文档字数")]
        public long TextLength
        {
            get
            {
                return File.TextLength;
            }
            set
            {

            }
        }

        public Document(IFile file)
        {
            File = file;
        }
    }

    public class DocumentCollection : BindingList<Document>
    {

    }
}
