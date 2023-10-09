using Android.App;
using Android.Runtime;
using BarcodeSDK.MAUI.Droid.Services;

namespace UseCases.MAUI;

[Application]
public class MainApplication : MauiApplication
{

    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp()
    {
        DependencyManager.RegisterServices();
        return MauiProgram.CreateMauiApp();
    }
}