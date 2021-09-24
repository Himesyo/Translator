using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.IO;
using Himesyo.Translation;
using Himesyo.Win32;

namespace Himesyo.DocumentTranslator
{
    public partial class UcFileInfo : Form
    {
        private static readonly Icon defaultIcon = Resources.ExtractIcon("shell32.dll", 0);

        public UcFileInfo()
        {
            InitializeComponent();
            TopLevel = false;
        }

        public int Index { get; set; }
        public Document Document { get; set; }

        public MessageValue ShowMessageMessage { get; } = new MessageValue();

        public string FileName
        {
            get => labelName.Text;
            set => labelName.Text = value;
        }
        public string FilePath
        {
            get => labelPath.Text;
            set => labelPath.Text = value;
        }
        public string FileState
        {
            get => labelState.Text;
            set => labelState.Text = value;
        }
        public string ShowMessage
        {
            get => labelMessage.Text;
            set => labelMessage.Text = value;
        }

        private bool init;

        public void RefreshShow()
        {
            pictureBox1.Image = defaultIcon.ToBitmap();
            if (init)
            {

            }
        }

        public void RenderNext()
        {
            FileName = Document.Name;
            FilePath = Document.FullPath;
            FileState = Document.State;
            if (ShowMessageMessage.NeedRefresh)
            {
                ShowMessage = ShowMessageMessage.Message;
                ShowMessageMessage.NeedRefresh = false;
            }
            if (Document.ProgressStyle != progressBar1.Style)
            {
                progressBar1.Style = Document.ProgressStyle;
                progressBar1.Maximum = int.MaxValue;
            }
            if (progressBar1.Style != ProgressBarStyle.Marquee)
            {
                double progress = Document.Progress;
                progress = progress < 0d ? 0d : progress;
                progress = progress > 1d ? 1d : progress;
                progressBar1.Value = (int)(progress * int.MaxValue);
            }
        }

        private void UcFileInfo_Load(object sender, EventArgs e)
        {
            RenderNext();
        }

        private void labelPath_Click(object sender, EventArgs e)
        {
            string path = labelPath.Text.FormatEmpty();
            FileHelper.ShowInExplorer(path);
        }

        private async void UcFileInfo_Shown(object sender, EventArgs e)
        {
            ActionInfo info = new ActionInfo();
            InitRunMessage initRun = new InitRunMessage();
            initRun.AddMessageValue(ShowMessageMessage);
            await Document.File.InitAsync(info, initRun);
            init = true;
            RefreshShow();
        }
    }
}
