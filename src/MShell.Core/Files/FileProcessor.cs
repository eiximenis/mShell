using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MShell.Core.Files
{
    public class FileProcessor
    {
        public IEnumerable<ShellFile> Entries(string path, string filter)
        {
            var items = Directory.EnumerateFileSystemEntries(path, filter, SearchOption.TopDirectoryOnly);
            foreach (var item in items)
            {
                yield return new ShellFile(new FileInfo(item));
            }
        }
    }
}
