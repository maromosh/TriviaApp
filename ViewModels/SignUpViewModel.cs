using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class SignUpViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;

        public SignUpViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            this.NameError = "This is a required field";
            this.PasswordError = "This is a required field";
            this.EmailError = "Invalid Email";
            this.SignUpCommand = new Command(OnSignUp);
        }

        #region Name
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string nameSignUp;
        public string NameSignUp
        {
            get => nameSignUp;
            set
            {
                nameSignUp = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;
        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(NameSignUp);
        }
        #endregion

        #region Email
        private bool showEmailError;

        public bool ShowEmailError//ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string emailSignUp;

        public string EmailSignUp
        {
            get => emailSignUp;
            set
            {
                emailSignUp = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            string email1 = EmailSignUp;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email1);
            this.ShowEmailError = !match.Success;
        }
        #endregion

        #region passWord
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string passwordSignUp;

        public string PasswordSignUp
        {
            get => passwordSignUp;
            set
            {
                passwordSignUp = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(PasswordSignUp);
        }
        #endregion

        private bool ValidateForm()
        {
            ValidateEmail();
            ValidatePassword();
            ValidateName();
            if (ShowEmailError || ShowNameError || ShowPasswordError)
            {
                return false;
            }
            return true;
        }

        public ICommand SignUpCommand { get; set; }
        private async void OnSignUp()
        {
            if (ValidateForm())
            {
                User user = new User()
                {
                    Name = this.nameSignUp,
                    Email = this.emailSignUp,
                    Password = this.passwordSignUp
                };
                bool succes = await triviaService.RegisterUser(user);

                if (!succes)
                {
                    await App.Current.MainPage.DisplayAlert("Sign up", "Sign up Faild!", "ok");
                }
                else
                {
                    await App.Current.MainPage.Navigation.PopAsync();
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Sign up", "Sign up Faild!", "ok");
            }
        }


    }

}
