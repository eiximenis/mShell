using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Commands.Builtin
{
    public class ChdirOptions
    {
        [Value(0, MetaName = "Path to navigate", HelpText = "Path to navigate")]
        public string PathToChange { get; set; }
    }

    public class Chdir : BuiltinCommand<ChdirOptions>
    {
        protected override void Run(ShellCommand command, ShellContext context, ChdirOptions options)
        {
            
        }
    }
}
