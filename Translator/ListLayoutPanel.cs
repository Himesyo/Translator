using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Himesyo.DocumentTranslator
{
    //[DefaultProperty("FlowDirection")]
    [Docking(DockingBehavior.Ask)]
    public partial class ListLayoutPanel : UserControl
    {
        public ListLayoutPanel()
        {
            InitializeComponent();
        }

        public override LayoutEngine LayoutEngine
        {
            get => ListLayoutEngine.Instance;
        }
    }

    public class ListLayoutEngine : LayoutEngine
    {
        public static readonly ListLayoutEngine Instance = new ListLayoutEngine();

        public override void InitLayout(object child, BoundsSpecified specified)
        {
            base.InitLayout(child, specified);
        }

        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            Control parent = container as Control;

            Rectangle parentDisplayRectangle = parent.DisplayRectangle;
            Point nextControlLocation = parentDisplayRectangle.Location;
            int parentUsableWidth = parent.ClientSize.Width - parent.Padding.Horizontal;
            int nextUsableWidth = parentUsableWidth;

            foreach (Control c in parent.Controls)
            {
                if (!c.Visible)
                {
                    continue;
                }

                nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                nextUsableWidth -= c.Margin.Horizontal;

                c.Location = nextControlLocation;

                Size newSize = c.AutoSize ? c.GetPreferredSize(parentDisplayRectangle.Size) : c.Size;
                newSize = new Size(nextUsableWidth, newSize.Height);
                if (newSize != c.Size)
                {
                    c.Size = newSize;
                }

                nextControlLocation.X = parentDisplayRectangle.X;

                nextControlLocation.Y += c.Height + c.Margin.Bottom;

                nextUsableWidth = parentUsableWidth;
            }
            return false;
        }
    }
}
