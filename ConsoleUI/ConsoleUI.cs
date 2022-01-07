using System;
using System.IO;
using DFMLib;
using Microsoft.Extensions.DependencyInjection;
using Pl;

namespace PL
{
    internal class ConsoleUI
    {
        private static void Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<InputCommand>().UserInput();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<InputCommand>();
        }
    }
}
