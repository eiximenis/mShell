using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MShell.Core
{
    public class ShellBuilder : IShellBuilder
    {
        private readonly ConfigurationProvider _configProvider;
        public ShellBuilder()
        {
            _configProvider = new ConfigurationProvider();
        }


        public static IShellBuilder CreateDefaultBuilder()
        {
            var builder = new ShellBuilder() as IShellBuilder;
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".mshell");
            builder.UseConfiguration(cfg => cfg.AddConfigFile(Path.Combine(folder,"mshell.json")));

            return builder;
        }

        IShellBuilder IShellBuilder.UseConfiguration(Action<IConfigurationProvider> configAction)
        {
            configAction(_configProvider);
            return this;
        }

        Shell IShellBuilder.Build()
        {

            var config = _configProvider.Load();

            var shell = new Shell(config);

            return shell;
        }
    }
}
