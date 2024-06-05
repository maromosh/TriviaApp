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
        { // פעולה בונה אשר קוראת את כל המשתמשים בעזדרת פעולת עזר
            triviaService = service;
            this.Users = new ObservableCollection<User>();
            ReadUsers();
        }

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
        
        private async void ReadUsers()
        {
            List<User> list = await triviaService.GetAllUsers();
            list = list.OrderByDescending(u => u.Score).ToList();
            this.Users = new ObservableCollection<User>(list);
        }
    }
}
