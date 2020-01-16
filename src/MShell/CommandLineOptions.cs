using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell
{
    
    public class CommandLineOptions
    {
        [Option("plugins-path", Required = false,Separator =';',  HelpText = "Set plugins paths.")]
        public IEnumerable<string> PluginsPath { get; set; }
    }
}
