using MShell.Core.Commands;
using MShell.Core.Configuration;
using MShell.Core.Input;
using MShell.Plugins.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MShell.Core
{
    public class Shell 
    {
        private readonly MainConfiguration _cfg;
        private readonly Dictionary<string, IInnerShell> _innerShells;

        private IInnerShell _current;

        private string Prompt
        {
            get
            {
                return _current?.Prompt ?? ">";
            }
        }

        public Shell(MainConfiguration config)
        {
            _cfg = config;
            _innerShells = new Dictionary<string, IInnerShell>();
            _current = null;
        }

        public void Start()
        {
            ProcessConfiguration();
            var exit = false;
            var processor = new InputProcessor();
            var handler = new CommandHandler();
            Console.Clear();
            do
            {
                var cmd = processor.Read(Prompt);
                if (cmd != null)
                {
                    if (cmd.IsInnerShell)
                    {
                        RunInnerShellCommand(cmd);
                    }
                    else
                    {
                        handler.Run(cmd);
                    }
                }
            } while (!exit);
        }

        private void RunInnerShellCommand(ShellCommand cmd)
        {
            var shell = GetInnerShell(cmd.ProcessedValue);
            if (shell != null)
            {
                shell.Enter();
                _current = shell;
            }
        }

        private void ProcessConfiguration()
        {
            foreach (var pluginPath in _cfg.PluginPaths)
            {
                var files = Directory.GetFiles(pluginPath, "*.dll");
                foreach (var file in files)
                {
                    var assembly = Assembly.LoadFile(file);
                    var pluginTypes =
                        assembly.GetExportedTypes()
                        .Where(t => t.GetInterfaces().Any(ti => ti == typeof(IInnerShell)));
                    foreach (var pluginType in pluginTypes)
                    {
                        AddInnerShell(pluginType);
                    }
                }
            }
        }

        private void AddInnerShell(Type pluginType)
        {
            var attr = pluginType.GetCustomAttribute<InnerShellAttribute>();
            if (attr == null)
            {
                Console.Error.WriteLine("Error: InnerShell do not have attribute");
                return;
            }
            _innerShells.Add(attr.EntryCommand, Activator.CreateInstance(pluginType) as IInnerShell);
        }

        private IInnerShell GetInnerShell(string entryCommand)
        {
            return _innerShells.TryGetValue(entryCommand, out var innerShell) ? innerShell : null;
        }
    }
}
