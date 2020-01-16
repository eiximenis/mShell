using CommandLine;
using MShell.Core;
using MShell.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MShell
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = ShellBuilder.CreateDefaultBuilder();

            if (args.Any())
            {
                var r = Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .WithParsed<CommandLineOptions>(clo =>
                    {
                        ApplyParsedArguments(clo, builder);
                    });
            }


            var shell = builder.Build();
            shell.Start();

        }

        private static void ApplyParsedArguments(CommandLineOptions clo, IShellBuilder builder)
        {
            if (clo.PluginsPath?.Any() == true)
            {
                foreach (var path in clo.PluginsPath)
                {
                    builder.UseConfiguration(sc =>
                    {
                        sc.AddConfig(new SerializedConfiguration()
                        {
                            InnerShells = new List<InnerShellConfiguration>()
                            {
                                new InnerShellConfiguration() { Path = path}
                            }
                        });
                    });
                }
            }
        }
    }
}
