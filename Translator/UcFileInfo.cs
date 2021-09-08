using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.Win32;

namespace Himesyo.DocumentTranslator
{
    public partial class UcFileInfo : UserControl
    {
        private static readonly Icon defaultIcon = Resources.ExtractIcon("shell32.dll", 0);

        public UcFileInfo()
        {
            InitializeComponent();
        }

        public int Index { get; set; }
        public Document Document { get; set; }

        public void RefreshShow()
        {
            pictureBox1.Image = defaultIcon.ToBitmap();
        }
    }
}
