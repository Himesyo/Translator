using System;
using System.Collections;
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
    public partial class UcFileBox : UserControl
    {
        public UcFileBox()
        {
            InitializeComponent();
        }

        private readonly Freezer sizeChangedFreezer = false;
        private void Control_SizeChanged(object sender, EventArgs e)
        {
            if (sizeChangedFreezer)
                return;

            Control control = sender as Control;
            using (Recovery sizeChangedRecovery = new Recovery(sizeChangedFreezer))
            {
                Size sizeSource = control.Size;
                control.Size = new Size(sizeSource.Width, sizeSource.Height);
            }
        }

        private int CalculationUsableWidth()
        {
            int width = ClientSize.Width;
            width -= Padding.Horizontal;
            return width - 6;
        }

        public void RefreshLayout()
        {
            int width = CalculationUsableWidth();
            int x = Padding.Left;
            int y = Padding.Top - VerticalScroll.Value;
            using (Recovery sizeChangedRecovery = new Recovery(sizeChangedFreezer))
            {
                foreach (var item in Controls)
                {
                    if (item is UcFileInfo ucFile)
                    {
                        Padding margin = ucFile.Margin;
                        margin.Left = 0;
                        margin.Right = 0;
                        ucFile.Margin = margin;
                        y += ucFile.Margin.Top;
                        Size sizeSource = ucFile.Size;
                        ucFile.Size = new Size(width, sizeSource.Height);
                        ucFile.Location = new Point(x + 3, y);
                        y += Margin.Bottom;
                        y += sizeSource.Height;
                    }
                }
            }
            y += Padding.Bottom;
            PerformLayout();
            //if (y > ClientSize.Height)
            //{
            //    VerticalScroll.Visible = true;
            //    VerticalScroll.Maximum = y;
            //    VerticalScroll.LargeChange = ClientSize.Height;
            //    HorizontalScroll.Visible = false;
            //    HorizontalScroll.Maximum = 0;
            //}
            //else
            //{
            //    VerticalScroll.Visible = true;
            //    VerticalScroll.Maximum = ClientSize.Height;
            //    VerticalScroll.LargeChange = ClientSize.Height;
            //    HorizontalScroll.Visible = false;
            //    HorizontalScroll.Maximum = 0;
            //}
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (e.AffectedControl is UcFileInfo ucFile)
            {
                switch (e.AffectedProperty)
                {
                    default:
                        RefreshLayout();
                        break;
                }
            }
            else if (e.AffectedControl == this)
            {
                RefreshLayout();
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is UcFileInfo ucFile)
            {
                ucFile.SizeChanged += Control_SizeChanged;
            }
            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (e.Control is UcFileInfo ucFile)
            {
                ucFile.SizeChanged -= Control_SizeChanged;
            }
            base.OnControlRemoved(e);
        }

    }
}
