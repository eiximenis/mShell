using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Commands.Builtin
{
    public class PwdOptions
    {
        [Option('w', Required = false, HelpText = "Show paths in Windows format")]
        public bool UseWindowsFormat { get; set; }

        public PwdOptions()
        {
            UseWindowsFormat = true;
        }
    }
    public class Pwd : BuiltinCommand<PwdOptions>
    {
        protected override void Run(ShellCommand command, ShellContext context, PwdOptions options)
        {
            
        }
    }
}
