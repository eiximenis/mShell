using System;
using System.IO;

namespace MShell.Core.Files
{
    public struct ShellFile
    {
        public bool IsRealFile { get; }
        public string Name { get; }
        public long Size { get; }
        public FileAttributes Attributes { get; }
        public DateTime LastModified { get; }

        public ShellFile(FileInfo fileInfo)
        {
            Name = fileInfo.Name;
            IsRealFile = true;
            Attributes = fileInfo.Attributes;
            if ((Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Size = 0;
            }
            else
            {
                Size = fileInfo.Length;
            }
            LastModified = fileInfo.LastWriteTime;
        }

        public ShellFile (string name, DateTime lastModified)
        {
            Name = name;
            IsRealFile = false;
            Size = 0;
            Attributes = FileAttributes.Device;
            LastModified = lastModified;
        }
    }
}