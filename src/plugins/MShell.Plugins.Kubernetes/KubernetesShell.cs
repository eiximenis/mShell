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

        public void Enter()
        {
            Console.WriteLine("+++ MShell Kubernetes plugin +++");
        }
    }
}
