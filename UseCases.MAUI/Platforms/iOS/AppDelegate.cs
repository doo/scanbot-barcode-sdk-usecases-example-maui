using BarcodeSDK.MAUI.iOS.Services;
using Foundation;
using SQLitePCL;

namespace UseCases.MAUI;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp()
    {
        DependencyManager.Register();
        raw.SetProvider(new SQLite3Provider_sqlite3());

        return MauiProgram.CreateMauiApp();
    }
}

