using TriviaAppClean.Models;

namespace TriviaAppClean;

public partial class App : Application
{
	//Use this class to store global application data that should be accessible throughout the entire app!
	public User LoggedInUser { get; set; }
	public App()
	{
		LoggedInUser = null;
		InitializeComponent();

		MainPage = new AppShell();
	}
}
