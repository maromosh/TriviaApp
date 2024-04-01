using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class ManagerAppShell : Shell
{
    public ManagerAppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
        Routing.RegisterRoute("theGame", typeof(TheGameView));
    }
}
