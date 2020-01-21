using MShell.Core.Files;
using MShell.Plugins.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MShell.Plugins.Processes
{
    class ProcessesVirtualFileTree : IVirtualFileTree
    {
        public string MountPath => "proc";

        public IEnumerable<string> GetEntries(in ShellPath path)
        {
            return Process.GetProcesses()
                .Select(p => p.ProcessName);
        }
    }
}