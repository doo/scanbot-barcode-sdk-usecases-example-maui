namespace UseCases.MAUI;

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

        ScanbotSDK.MAUI.ScanbotBarcodeSDK.Initialize(builder, new ScanbotSDK.MAUI.Models.InitializationOptions
        {

            LicenseKey = "",
            LoggingEnabled = true,
            ErrorHandler = (status, feature) =>
            {
                Console.WriteLine($"License error: {status}, {feature}");
            },
        });

        return builder.Build();
    }
}
