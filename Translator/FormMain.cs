using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.IO;
using Himesyo.Translation;

namespace Himesyo.DocumentTranslator
{
    public partial class FormMain : Form
    {
        public static readonly string TranslatorTypesPath = "TranslatorTypes";
        public static readonly string FileTypesPath = "FileTypes";

        public static Dictionary<TName, ITranslatorType> TranslatorTypes { get; } = new Dictionary<TName, ITranslatorType>();
        public static Dictionary<FName, IFileType> FileTypes { get; } = new Dictionary<FName, IFileType>();

        public static BindingList<ITranslator> Translators { get; } = new BindingList<ITranslator>();
        public static DocumentCollection Documents { get; } = new DocumentCollection();

        private static Dictionary<int, IFileType> IndexFileType { get; } = new Dictionary<int, IFileType>();

        /// <summary>
        /// 获取指定类型翻译器的存放目录。不自动创建目录。
        /// </summary>
        /// <param name="translatorType"></param>
        /// <returns></returns>
        private static string GetTranslatorFolder(ITranslatorType translatorType)
        {
            string translatorRoot = "Translators";
            string nameDir = Path.Combine(translatorRoot, TypeDescriptor.GetClassName(translatorType));
            return nameDir;
        }
        /// <summary>
        /// 获取指定翻译器应该保存的位置。自动创建目录。
        /// </summary>
        /// <param name="translator"></param>
        /// <returns></returns>
        private static string GetTranslatorPath(ITranslator translator)
        {
            ITranslatorType translatorType = TranslatorTypes[translator.TypeName];
            string nameDir = GetTranslatorFolder(translatorType);
            Directory.CreateDirectory(nameDir);
            string fileName = $"t{PathConvert.GetSafeFileName(translator.FileName)}.translator";
            string path = Path.Combine(nameDir, fileName);
            return path;
        }
        /// <summary>
        /// 保存翻译器到文件。
        /// </summary>
        /// <param name="translator"></param>
        public static void SaveTranslator(ITranslator translator)
        {
            ITranslatorType translatorType = TranslatorTypes[translator.TypeName];
            IFormatter formatter = translatorType.GetSerializer();
            string path = GetTranslatorPath(translator);
            try
            {
                using (Stream stream = File.OpenWrite(path))
                {
                    formatter.Serialize(stream, translator);
                    stream.SetLength(stream.Position);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存文件失败。{ex.Message}", "翻译", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 从磁盘中删除翻译器文件。
        /// </summary>
        /// <param name="translator"></param>
        public static void DeleteTranslator(ITranslator translator)
        {
            string path = GetTranslatorPath(translator);
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除文件失败。{ex.Message}", "翻译", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public FormMain()
        {
            InitializeComponent();
        }

        private void LoadTranslatorTypes()
        {
            if (Directory.Exists(TranslatorTypesPath))
            {
                foreach (var item in Directory.GetFiles(TranslatorTypesPath, "*.dll"))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(item);
                    }
                    catch
                    {

                    }
                }
            }
            Type translatorType = typeof(ITranslatorType);
            var select = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                         from type in assembly.GetTypes()
                         where type.IsClass && !type.IsGenericType && translatorType.IsAssignableFrom(type)
                         let ctor = type.GetConstructor(Type.EmptyTypes)
                         where ctor != null
                         select ctor;
            foreach (var item in select)
            {
                ITranslatorType translator = (ITranslatorType)item.Invoke(new object[0]);
                TranslatorTypes.Add(translator.Name, translator);
            }
        }
        private void LoadTranslators()
        {
            foreach (var item in TranslatorTypes)
            {
                string dir = GetTranslatorFolder(item.Value);
                if (Directory.Exists(dir))
                {
                    IFormatter formatter = item.Value.GetSerializer();
                    foreach (var file in Directory.GetFiles(dir, "*.translator"))
                    {
                        try
                        {
                            using (Stream stream = File.OpenRead(file))
                            {
                                ITranslator translator = (ITranslator)formatter.Deserialize(stream);
                                Translators.Add(translator);
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        private void LoadFileTypes()
        {
            if (Directory.Exists(FileTypesPath))
            {
                foreach (var item in Directory.GetFiles(FileTypesPath, "*.dll"))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(item);
                    }
                    catch
                    {

                    }
                }
            }
            Type fileType = typeof(IFileType);
            var select = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                         from type in assembly.GetTypes()
                         where type.IsClass && !type.IsGenericType && fileType.IsAssignableFrom(type)
                         let ctor = type.GetConstructor(Type.EmptyTypes)
                         where ctor != null
                         select ctor;
            foreach (var item in select)
            {
                IFileType file = (IFileType)item.Invoke(new object[0]);
                FileTypes.Add(file.Name, file);
            }
        }
        private void LoadFiles()
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadTranslatorTypes();
            LoadTranslators();

            // 注册事件在 LoadTranslators(); 后面。
            Translators.ListChanged += Translators_ListChanged;

            LoadFileTypes();
            LoadFiles();

            var select = from type in FileTypes
                         where type.Value.OpenFileFilter != null
                         from filter in type.Value.OpenFileFilter
                         where filter != null
                         where filter.Extension == null || !filter.Extension.Contains("|")
                         where filter.Title == null || !filter.Extension.Contains("|")
                         select new { Filter = $"{filter.Title}|{filter.Extension}", FileType = type.Value };
            //select $"{filter.Title}|{filter.Extension}";
            StringBuilder filterString = new StringBuilder();
            int index = 1;
            foreach (var item in select)
            {
                IndexFileType[index++] = item.FileType;
                filterString.Append(item.Filter).Append('|');
            }
            openFileDialog1.Filter = filterString.Remove(filterString.Length - 1, 1).ToString();

            //FormTranslator form = new FormTranslator();
            //form.ShowDialog();

            Documents.Add(new Document(new TestFile()) { Progress = 0.12 });
            Documents.Add(new Document(new TestFile()) { Progress = 0.24 });
            Documents.Add(new Document(new TestFile()) { Progress = 0.56 });
            documentBindingSource.DataSource = Documents;
        }

        private void Translators_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                ITranslator translator = Translators[e.NewIndex];
                SaveTranslator(translator);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            int index = openFileDialog1.FilterIndex;
            IFileType fileType = IndexFileType[index];
            if (fileType.TryOpen(openFileDialog1.FileName, out var file, out var msg))
            {
                Documents.Add(new Document(file));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(msg))
                {
                    msg = "打开失败。";
                }
                MessageBox.Show($"{msg}\r\n\r\n来自 {fileType.Name}", "文档翻译");
                e.Cancel = true;
            }
        }

        private void menuBtnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}
