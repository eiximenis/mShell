using MShell.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Plugins.Core
{
    public interface IInnerShell
    {
        string Prompt { get; }

        void Enter();

        void Process(ShellCommand command);

        IVirtualFileTree VirtualFilesTree { get; }
    }
}
