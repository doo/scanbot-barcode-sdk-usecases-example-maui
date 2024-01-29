﻿namespace UseCases.MAUI;

public partial class App : Application
{
    private const string LICENSE_KEY = null;

    public App()
    {
        InitializeComponent();
        ScanbotSDK.MAUI.ScanbotBarcodeSDK.Initialize(new ScanbotSDK.MAUI.Models.InitializationOptions
        {
            LicenseKey = LICENSE_KEY,
            LoggingEnabled = true,
            ErrorHandler = (status, feature) =>
            {
                Console.WriteLine($"License error: {status}, {feature}");
            }
        });
        MainPage = new NavigationPage(new Pages.HomePage());
    }
}