using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupTimer.Models;
using StartupTimer.TimeProviders;
using StartupTimer.Views;
using StartupTimer.Views.Report;
using StartupTimer.Watchdogs;
using System.IO;

namespace StartupTimer {
    internal static class Program {
        static void Main() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true);

            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddSingleton(configuration);
            services.AddSingleton<WorkTimeProvider>();
            services.AddSingleton<TimeReport>();
            services.AddSingleton<ICurrentTimeProvider, CurrentTimeProvider>();
            services.AddTransient<ITimeProvider, BootUpTimeProvider>();
            services.AddTransient<ITimeProvider, LoginTimeProvider>();
            services.AddTransient<TimeWatchdogFactory>();
            services.AddTransient<IWatchdogScheme, OverworkWatchdogScheme>();
            services.AddTransient<IWatchdogScheme, AlmostDoneWorkWatchdogScheme>();
            services.AddTransient<NotifyWidget>();
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<Startup>();
            services.Configure<WorkConfiguration>(configuration.GetSection(WorkConfiguration.OPTION_NAME));

            var serviceProvider = services.BuildServiceProvider(true);
            var startup = serviceProvider.GetRequiredService<Startup>();
            startup.Run();
        }
    }
}
