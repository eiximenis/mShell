using MShell.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace MShell
{
    public interface IConfigurationProvider
    {
        IConfigurationProvider AddConfigFile(string path);
        IConfigurationProvider AddConfig(SerializedConfiguration configuration);
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly List<string> _paths;
        private readonly List<SerializedConfiguration> _configs;

        public ConfigurationProvider()
        {
            _paths = new List<string>();
            _configs = new List<SerializedConfiguration>();
        }

        IConfigurationProvider IConfigurationProvider.AddConfigFile(string path)
        {
            _paths.Add(path);
            return this;
        }



        IConfigurationProvider IConfigurationProvider.AddConfig(SerializedConfiguration configuration)
        {
            _configs.Add(configuration);
            return this;
        }


        public MainConfiguration Load()
        {
            var config = new MainConfiguration();
            foreach (var path in _paths)
            {
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    var pathConfig = JsonSerializer.Deserialize<SerializedConfiguration>(
                        json, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    config.MixWith(pathConfig);
                }
            }

            foreach (var additionalConfig in _configs)
            {
                config.MixWith(additionalConfig);
            }

            return config;
        }
    }
}
