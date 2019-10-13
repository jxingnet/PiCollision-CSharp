using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using XingSharpNet.ToolKit.Logger;
using System.Diagnostics;
/*
 * Pi collision simulator
 * 
 *  https://editor.p5js.org/codingtrain/sketches/4FuKfd-LJ
 * 
 * 
 */
namespace PiCollision
{
    class Program
    {
        static void Main(string[] args)
        {
            // enable logging in tools lib
            Log.GlobalLevel = XingSharpNet.ToolKit.Logger.LogLevel.All;
            Log.LoggerProvider = LoggerProviderType.NLogger;

            var _logger = LogManager.GetLogger("Main");

           
            //setup our DI
            var serviceCollection = new ServiceCollection();

            ConfigureServicesAsync(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _logger.Info("Starting application");

            var app = serviceProvider.GetRequiredService<PiCollision>();

            app.Test();

            _logger.Info("All done!");
        }

        private static void ConfigureServicesAsync(IServiceCollection serviceCollection)
        {           
            // add services
            serviceCollection
                //.AddSingleton<IConfig, Config>()
                //.AddSingleton<IConfig>(new Config())
                //.AddSingleton<IConfig, Config>(factory => { return new Config(new LoggerFactory(), "appsettings.json"); })
                //.AddTransient<TelegramBot>()
                .AddTransient<Sketch>()
                .AddTransient<PiCollision>()
                .AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog();
                });
        }
    }


    class PiCollision
    {
        Microsoft.Extensions.Logging.ILogger _logger;
        Sketch _sketch;
        public PiCollision(Sketch sketch, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Pi");
            _sketch = sketch;
        }

        public void Test()
        {
            _logger.LogInformation("Start...");

            var sw = new Stopwatch();
            sw.Start();
            while (!_sketch.draw())
            { 
            }
            sw.Stop();

            _logger.LogInformation($"Elapsed : {sw.Elapsed}");
            _logger.LogInformation("...end");
        }
    }
}
