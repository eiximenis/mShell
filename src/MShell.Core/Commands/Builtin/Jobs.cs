using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Commands.Builtin
{
    public class JobsOptions
    {
        [Option('a', Required = false, HelpText = "Show all jobs (including finished ones)")]
        public bool ShowAll { get; set; }
    }

    public class Jobs : BuiltinCommand<JobsOptions>
    {
        protected override void Run(ShellCommand command, ShellContext context, JobsOptions options)
        {
            foreach (var job in context.Jobs)
            {
                if (options.ShowAll || job.Running)
                {
                    var suffix = job.Running ? "running" : "exited";
                    Console.WriteLine($"[{job.Id}] - {job.ProcessName} {suffix}");
                }
            }
        }
    }
}
