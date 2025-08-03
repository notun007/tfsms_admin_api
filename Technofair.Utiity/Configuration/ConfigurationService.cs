using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        IConfiguration configuration;
        public ConfigurationService()
        {
            configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
        }

        public string GetSetting(string key)
        {
            var value = configuration[key];
            return value == null ? "" : value;
        }
    }

}
