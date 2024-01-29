namespace UseCases.MAUI;

public partial class App : Application
{
    private const string LICENSE_KEY = null;

    public App()
    {
        InitializeComponent();
     
        MainPage = new NavigationPage(new Pages.HomePage());
    }
}