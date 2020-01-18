using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Commands.Builtin
{
    public abstract class BuiltinCommand<TOptions> : IBuiltinCommand
    {
        public void Run(ShellCommand command, ShellContext context)
        {
            var result = Parser.Default.ParseArguments<TOptions>(new[] { command.ArgumentsAsString });
            switch (result)
            {
                case Parsed<TOptions> parsed:
                    Run(command, context, parsed.Value);
                    break;
                case NotParsed<TOptions> notParsed:
                    Error(command, notParsed.Errors);
                    break;
            }
        }

        protected abstract void Run(ShellCommand command, ShellContext context, TOptions options);

        protected abstract void Error (ShellCommand command, IEnumerable<Error> errors);
    }
}
