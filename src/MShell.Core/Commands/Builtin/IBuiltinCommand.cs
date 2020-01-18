using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Commands.Builtin
{
    public interface IBuiltinCommand
    {
        void Run(ShellCommand command, ShellContext context);
    }
}
