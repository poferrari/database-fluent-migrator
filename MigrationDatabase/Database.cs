using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MigrationDatabase
{
    public static class Database
    {
        private const string SqlApiConnectionStringName = "SqlApi";

        public static void RunMigrations(IConfiguration configuration)
        {
            var serviceProvider = CreateServices(configuration);

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using var scope = serviceProvider.CreateScope();
            RunMigrations(scope.ServiceProvider);
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices(IConfiguration configuration)
        {
            var connectionString = GetSqlApiConnectionString(configuration);

            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SqlServer support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Database).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void RunMigrations(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();

            // Execute the migrations
            //runner.MigrateUp();

            // Migrate Down 201109100935
            runner.MigrateDown(201109100935);
        }

        private static string GetSqlApiConnectionString(IConfiguration configuration)
           => configuration.GetConnectionString(SqlApiConnectionStringName);
    }
}
