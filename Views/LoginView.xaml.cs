using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}