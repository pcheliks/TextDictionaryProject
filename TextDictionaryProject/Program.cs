using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextDictionaryProject.Parsers;
using TextDictionaryProject.Repositories;
using TextDictionaryProject.Services;

namespace TextDictionaryProject
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Initialize();
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            var form = ServiceProvider.GetRequiredService<Form1>();
            Application.Run(form);
        }

        public static void Initialize()
        {
            global::System.Windows.Forms.Application.EnableVisualStyles();
            global::System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// Create a host builder to build the service provider
        /// </summary>
        /// <returns></returns>
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<Form1>();
                    var path = Directory.GetCurrentDirectory();
                    //var serilogLogger = new LoggerConfiguration()
                    //                .WriteTo.File($"{path}\\Logger\\logging.txt")
                    //                .CreateLogger();
                    //services.AddLogging(x =>
                    //{
                    //    x.SetMinimumLevel(LogLevel.Information);
                    //    x.AddSerilog(serilogLogger, dispose: true);
                    //});
                });
        }
    }
}
