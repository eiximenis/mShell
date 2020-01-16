using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Input
{
    class InputProcessor
    {
        public ShellCommand Read(string prompt)
        {
            Console.Write(prompt);
            var line = Console.ReadLine();
            return new ShellCommand(line);
        }
    }
}
