using MShell.Plugins.Core;
using System.Collections.Generic;
using System.Diagnostics;

namespace MShell.Plugins.Processes
{
    class ProcessesVirtualFileTree : IVirtualFileTree
    {
        public string MountPath => "proc";

        public IEnumerable<string> GetEntries()
        {
            var procs = Process.GetProcesses();
            foreach (var proc in procs)
            {
                yield return proc.ProcessName;
            }
        }
    }
}