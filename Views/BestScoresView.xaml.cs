using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class BestScoresView : ContentPage
{
	public BestScoresView(BestScoresViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}