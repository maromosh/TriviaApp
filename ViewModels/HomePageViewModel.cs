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
        public HomePageViewModel(TheGameView theGameView)
        {
            this.theGameView = theGameView;
            this.StartGameCommand = new Command(OnStartGame);
        }
        public ICommand StartGameCommand { get; set; } //- קומנד שמקושר לדף של המשחק ואם לוחצים עליו
        private async void OnStartGame() // נכנסים לפעולה אסינכרונית ופותחים בפופ את המשחק.
        {
            await Shell.Current.GoToAsync("theGame");
        }
       
    }
}
