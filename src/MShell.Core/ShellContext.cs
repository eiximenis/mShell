using MShell.Core.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core
{
    public class ShellContext
    {

        public string CurrentPath { get; set; }
        public ShellContext(string startupPath)
        {
            CurrentPath = startupPath;
            FileProcessor = new FileProcessor();
        }

        public FileProcessor FileProcessor { get; }
    }
}
