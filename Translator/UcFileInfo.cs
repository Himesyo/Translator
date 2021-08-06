using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Himesyo.DocumentTranslator
{
    public partial class UcFileInfo : UserControl
    {
        public UcFileInfo()
        {
            InitializeComponent();
        }

        public int Index { get; set; }
        public Document Document { get; set; }
    }
}
