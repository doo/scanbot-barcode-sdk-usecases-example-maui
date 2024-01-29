using Foundation;
using ScanbotSDK.MAUI.Services;
using SQLitePCL;

namespace UseCases.MAUI;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp()
    {
        DependencyManager.RegisterServices();
        raw.SetProvider(new SQLite3Provider_sqlite3());

        return MauiProgram.CreateMauiApp();
    }
}

