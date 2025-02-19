using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Mimica.Properties;
using Mimica.Services;

namespace Mimica
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<AppSettings>(context.Configuration.GetSection("AppSettings"));
                    services.AddTransient<FrmMain>();
                    services.AddTransient<IEventHooksService, EventHooksService>();
                    services.AddTransient<IScreenCaptureService, ScreenCaptureService>();
                    services.AddTransient<IEventLogService, EventLogService>();
                })
                .Build();

            ServiceProvider = host.Services;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationConfiguration.Initialize();
            Application.Run(ServiceProvider.GetRequiredService<FrmMain>());
        }
    }
}