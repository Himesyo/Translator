using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himesyo.Translation
{
    public interface IFileType
    {
        FName Name { get; }

        IReadOnlyCollection<Filter> OpenFileFilter { get; }

        bool TryOpen(string fileName, out IFile file, out string msg);
    }

    public interface IFile
    {
        FName TypeName { get; }

        string Name { get; }
        string FullPath { get; }

        long FileSize { get; }
        long TextLength { get; }
    }

    public class Filter
    {
        public string Extension { get; protected set; }
        public string Title { get; protected set; }

        protected Filter()
        {

        }
        public Filter(string extension, string title)
        {
            Extension = extension;
            Title = title;
        }
    }
}
