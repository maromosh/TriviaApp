using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class HomePageView : ContentPage
{
	public HomePageView(HomePageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}