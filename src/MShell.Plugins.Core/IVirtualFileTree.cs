using MShell.Core.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Plugins.Core
{
    public interface IVirtualFileTree
    {
        string MountPath { get; }

        IEnumerable<string> GetEntries(in ShellPath path);
    }
}
