using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class MasterAppShell : Shell
{
    public MasterAppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
    }
}