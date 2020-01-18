using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Configuration
{
    public class MainConfiguration
    {
        private readonly List<string> _pluginPaths;
        public IEnumerable<string> PluginPaths => _pluginPaths;

        public string StartupPath { get; private set; }

        public MainConfiguration()
        {
            _pluginPaths = new List<string>();
            StartupPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        public void MixWith(SerializedConfiguration cfg)
        {
            foreach (var ishell in cfg.InnerShells)
            {
                _pluginPaths.Add(ishell.Path);
            }
        }
    }
}
