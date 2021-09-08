using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.Logger;
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

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            LoggerSimple.Init("Logs", "Translator");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            if (LoggerSimple.CanWrite)
            {
                LoggerSimple.WriteError("捕获异常。", e.Exception);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (LoggerSimple.CanWrite)
            {
                LoggerSimple.WriteError("未处理捕获异常。", e.ExceptionObject as Exception);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (LoggerSimple.CanWrite)
            {
                LoggerSimple.WriteError("捕获线程异常。", e.Exception);
            }
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

        [DisplayName("当前进度")]
        public double Progress { get; set; }

        public Document(IFile file)
        {
            File = file;
        }
    }

    public class DocumentCollection : BindingList<Document>
    {

    }
}
