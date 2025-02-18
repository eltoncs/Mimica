using Microsoft.Extensions.DependencyInjection;
using Mimica.Services;

namespace Mimica
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IEventHooksService, EventHooksService>();
            serviceCollection.AddTransient<IScreenCaptureService, ScreenCaptureService>();
            serviceCollection.AddTransient<IEventLogService, EventLogService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMain
                (ServiceProvider.GetRequiredService<IEventHooksService>(),
                ServiceProvider.GetRequiredService<IScreenCaptureService>(),
                ServiceProvider.GetRequiredService<IEventLogService>()));
        }
    }
}