using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class UserAppShell : Shell
{
	public UserAppShell()
	{
		InitializeComponent();
		RegisterRoutes();
	}

	private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
    }
}
