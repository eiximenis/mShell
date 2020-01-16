using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MShell.Core.Commands
{
    public class CommandHandler
    {
        public void Run(ShellCommand command)
        {

            var p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                FileName = command.RawLine
            };
            p.Start();
        }
    }
}
