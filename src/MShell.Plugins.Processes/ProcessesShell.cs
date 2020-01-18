using MShell.Core;
using MShell.Plugins.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Plugins.Processes
{
    [InnerShell(EntryCommand = "proc", Name = "Kubernetes Shell")]
    public class ProcessesShell : IInnerShell
    {
        public string Prompt => "proc>";

        public IVirtualFileTree VirtualFilesTree { get; }

        public ProcessesShell()
        {
            VirtualFilesTree = new ProcessesVirtualFileTree();
        }

        public void Enter() { }

        public void Process(ShellCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
