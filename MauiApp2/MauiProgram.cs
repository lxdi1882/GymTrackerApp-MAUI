
using Microsoft.Extensions.Logging;
using MauiApp2.Data;
using MauiApp2.Services;
using MauiApp2.Views;

namespace MauiApp2;


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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<MainPage>();   // see note below
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}