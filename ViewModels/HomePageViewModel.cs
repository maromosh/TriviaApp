using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class HomePageViewModel :ViewModelBase
    {
        private TheGameView theGameView;
        public ICommand StartGameCommand { get; set; }
        private async void OnStartGame()
        {
            await Application.Current.MainPage.Navigation.PushAsync(theGameView);
        }
    }
}
