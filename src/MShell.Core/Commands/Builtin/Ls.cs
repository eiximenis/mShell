using CommandLine;
using MShell.Core.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MShell.Core.Commands.Builtin
{

    public class LsOptions
    {
        [Option('l', Required = false, HelpText ="Uses long format")]
        public bool UseLongFormat { get; set; }
        [Value(0, MetaName = "File or Path Name", HelpText = "File Or Path Name")]
        public string PathToUse { get; set; }
        [Value(1, MetaName = "Filter", HelpText = "Filter to use (i.e. *.*)")]
        public string Filter { get; set; }
    }

    public class Ls : BuiltinCommand<LsOptions>
    {

        protected override void Run(ShellCommand command, ShellContext context, LsOptions options)
        {
            if (!string.IsNullOrEmpty(options.PathToUse))
            {
                var newPath = new ShellPath(options.PathToUse);
                if (!newPath.IsAbsolute)
                {
                    newPath = ShellPath.Combine(in context.CurrentPath, in newPath);
                }
                Process(in newPath, context, options);
            }
            else
            {
                Process(in context.CurrentPath, context, options);
            }


        }

        private void Process(in ShellPath path, ShellContext context, LsOptions options)
        {
            var filter = string.IsNullOrEmpty(options.Filter) ? "*.*" : options.Filter;
            var entries = context.FileProcessor.Entries(path, filter);

            foreach (var file in entries)
            {
                Console.WriteLine(file.Name);
            }
        }
    }
}
