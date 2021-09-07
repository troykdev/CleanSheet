using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CleanSheet
{
    class ConfigHelper
    {
        public static List<WatcherRule> LoadConfigFiles()
        {
            string[] fileEntries = Directory.GetFiles(Path.Join(Directory.GetCurrentDirectory(), "/rules/"));
            List<WatcherRule> rules = new List<WatcherRule>();
            foreach (string fileName in fileEntries)
            {
                rules.Add(SerializeConfig(fileName));
            }
            return rules;
                
        }

        private static WatcherRule SerializeConfig(string path)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)  // see height_in_inches in sample yml 
                .Build();
            var rule = deserializer.Deserialize<WatcherRule>(File.ReadAllText(path));
            return rule;
        }
    }
}
