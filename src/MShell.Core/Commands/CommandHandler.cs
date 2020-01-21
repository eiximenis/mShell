using MShell.Core.Commands.Builtin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MShell.Core.Commands
{
    public class CommandHandler
    {
        private Dictionary<string, IBuiltinCommand> _builtinCommands;

        public CommandHandler()
        {
            _builtinCommands = new Dictionary<string, IBuiltinCommand>();
            _builtinCommands.Add("ls", new Ls());
            _builtinCommands.Add("cd", new Ls());
            _builtinCommands.Add("jobs", new Jobs());
        }


        public void Run(ShellCommand command, ShellContext context)
        {
            var builtin = FindBuiltinCommand(command);
            if (builtin is null)
            {
                RunExternalCommand(command, context);
            }
            else
            {
                builtin.Run(command, context);
            }
        }

        private void RunExternalCommand(ShellCommand command, ShellContext context)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                FileName = command.CommandName,
                Arguments = command.ArgumentsAsString
            };
            
            if (command.IsJob)
            {
                context.AddJob(new ShellJob(process));
            }
            else
            {
                process.Start();
                process.WaitForExit();
            }
        }

        private IBuiltinCommand FindBuiltinCommand(ShellCommand command)
        {
            var key = command.CommandName;
            return _builtinCommands.TryGetValue(key, out var builtinCommand) ? builtinCommand : null;
        }
    }
}
