using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MigrationDatabase
{
    static class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = BuildConfiguration();

            Database.RunMigrations(configuration);

            Console.WriteLine("Run!");
        }

        private static IConfiguration BuildConfiguration()

            => new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
    }
}
