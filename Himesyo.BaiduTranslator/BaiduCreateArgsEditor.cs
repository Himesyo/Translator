using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.Translation;

namespace Himesyo.BaiduTranslator
{
    public class BaiduCreateArgsEditor : ICreateArgsEditor
    {
        public TName TypeName => BaiduTranslatorType.TypeName;

        public ICreateArgs EditArgs
        {
            get => editor.SelectedObject as ICreateArgs;
            set => editor.SelectedObject = value;
        }
        private PropertyGrid editor;
        public Control ShowControl => editor;

        public BaiduCreateArgsEditor()
        {
            editor = new PropertyGrid();
        }
    }
}
