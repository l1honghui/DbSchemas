

namespace DbSchemas.Service.Configuration
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConfigHelper
    {
        static ConfigHelper()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(ProcessDirectory)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static string ProcessDirectory
        {
            get
            {
                return AppContext.BaseDirectory;
            }
        }

        public static IConfiguration Configuration { get; }

    }
}
