using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class BestScoresViewModel: ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;

        public BestScoresViewModel(TriviaWebAPIProxy service)
        {

            triviaService = service;
            ReadUsers();
        }

        private string name;
        private int score;


        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            { return users; }
            set
            {
                this.users = value;
                OnPropertyChanged();
            }
        }
        private TriviaWebAPIProxy userService;
        public TriviaWebAPIProxy UserService
        {
            get { return userService; }
            set
            {
                this.userService = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;

            }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set { score = value; }
        }
        private async void ReadUsers()
        {
            TriviaWebAPIProxy service = this.userService;
            List<User> list = await service.GetAllUsers();
            list = list.OrderByDescending(u => u.Score).ToList();
            this.Users = new ObservableCollection<User>(list);
        }
    }
}
