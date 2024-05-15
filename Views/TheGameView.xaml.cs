using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class TheGameView : ContentPage
{
	public TheGameView(TheGameViewModel vm)
	{
        InitializeComponent();
		this.BindingContext = vm;
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PopModalAsync();
    }
}