using MShell.Core;
using MShell.Plugins.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Plugins.Kubernetes
{
    [InnerShell(EntryCommand = "k8s", Name = "Kubernetes Shell")]
    public class KubernetesShell : IInnerShell
    {
        public string Prompt => "k8s>";

        public IVirtualFileTree VirtualFilesTree => null;

        public void Enter()
        {
            Console.WriteLine("+++ MShell Kubernetes plugin +++");
        }

        public void Process(ShellCommand command)
        {

        }
    }
}
