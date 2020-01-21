using MShell.Plugins.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MShell.Core.Files
{
    public class FileProcessor
    {
        private readonly Dictionary<string, IVirtualFileTree> _virtualTrees;

        public FileProcessor()
        {
            _virtualTrees = new Dictionary<string, IVirtualFileTree>();
        }

        public void MountVirtualFileProcessor(IVirtualFileTree virtualFileTree)
        {
            _virtualTrees.Add(virtualFileTree.MountPath, virtualFileTree);
        }

        public IEnumerable<ShellFile> Entries(in ShellPath path, string filter)
        {
            if (path.IsVirtual)
            {
                var vpath = path.Value;
                var sep = path.Value.IndexOf('\\');
                if (sep != -1)
                {
                    vpath = path.Value.Substring(0, sep);
                }
                if (_virtualTrees.TryGetValue(vpath, out var vtree))
                {
                    return vtree.GetEntries(path)
                        .Select(p => new ShellFile(p, DateTime.UtcNow));
                }

                return Enumerable.Empty<ShellFile>();
            }
            else
            {

                var items = Directory.EnumerateFileSystemEntries(path.Value, filter, SearchOption.TopDirectoryOnly)
                    .Select(pn => new ShellFile(new FileInfo(pn)));

                return items;
            }
        }
    }
}
