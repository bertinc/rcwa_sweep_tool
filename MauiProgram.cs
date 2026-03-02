using Microsoft.Extensions.Logging;
using RCWA_Sweep_Tool.Services;

namespace RCWA_Sweep_Tool
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // Register shared services and load user defaults
            builder.Services.AddSingleton<DesignParametersService>(sp =>
            {
                var service = new DesignParametersService();
                service.LoadDefaults(); // Load saved defaults on startup
                return service;
            });

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
