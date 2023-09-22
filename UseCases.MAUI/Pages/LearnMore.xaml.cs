namespace UseCases.MAUI.Pages;

public partial class LearnMore : ContentView
{
	public LearnMore()
	{
		InitializeComponent();

        var recognizer = new TapGestureRecognizer();
        recognizer.Tapped += TrialClicked;

        trialLink.GestureRecognizers.Add(recognizer);

    }

    void SupportClicked(System.Object sender, System.EventArgs e)
    {
        Browser.OpenAsync("https://docs.scanbot.io/support");
    }

    void TrialClicked(System.Object sender, System.EventArgs e)
    {
        Browser.OpenAsync("https://scanbot.io/trial");
    }
}
