using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class LoginViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        private SignUpView signUpView;
        private ManagerAppShell managerAppShell;
        private UserAppShell userAppShell;
        private MasterAppShell masterAppShell;

        public LoginViewModel(TriviaWebAPIProxy service, SignUpView sigunUpView, ManagerAppShell managerShell, UserAppShell userAppShell, MasterAppShell masterAppShell)
        { // פעולה בונה אשר מכניסה ערכים התחלתיים
            this.signUpView = sigunUpView;
            InServerCall = false;
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
            this.SignUpCommand = new Command(OnSignUp);
            this.PasswordError = "This is a required field";
            this.EmailError = "Invalid Email";
            this.managerAppShell = managerShell;
            this.userAppShell = userAppShell;
            this.masterAppShell = masterAppShell;
        }

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

        private string emailLogin;

        public string EmailLogin
        {
            get => emailLogin;
            set
            {
                emailLogin = value;
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
        //private string writeEmailError = "";
        //public string WriteEmailError
        //{
        //    get => writeEmailError;
        //    set
        //    {
        //        writeEmailError = value;
        //        OnPropertyChanged("EmailError");
        //    }
        //}
        //private string writePasswordError = "";
        //public string WritePasswordError
        //{
        //    get => writePasswordError;
        //    set
        //    {
        //        writePasswordError = value;
        //        OnPropertyChanged("PasswordError");
        //    }
        //}
        //public void WriteErrors()
        //{
        //    if (ShowEmailError == true)
        //    {
        //        writeEmailError = "You have an error in your Email";
        //        Console.WriteLine(writeEmailError);
        //    }
        //    else if (ShowPasswordError == true)
        //    {
        //        writePasswordError = "You have an error in your password";
        //        Console.WriteLine(writePasswordError);
        //    }
        //}

        private void ValidateEmail()
        {
            string email = EmailLogin;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
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
         

        private string passwordLogin;

        public string PasswordLogin
        {
            get => passwordLogin;
            set
            {
                passwordLogin = value;
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
            this.ShowPasswordError = string.IsNullOrEmpty(PasswordLogin);
        }
        #endregion
        private void ValidateForm()
        { 
            ValidateEmail();
            ValidatePassword();
        }
        public ICommand LoginCommand { get; set; }
        private async void OnLogin()
        {
            ValidateForm();
            if (ShowEmailError || ShowPasswordError)
            {
                await Console.Out.WriteLineAsync("You have an error");
                return;
            }
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall=true;
            await Application.Current.MainPage.Navigation.PushModalAsync(new ConnectingToServerView());
            User u  = await this.triviaService.LoginAsync(EmailLogin, PasswordLogin);
            await Application.Current.MainPage.Navigation.PopModalAsync();
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Login Faild!", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", $"Login Succeed!", "ok");
                if (u.Rank == 2) //Admin
                {
                    Application.Current.MainPage = managerAppShell;
                }
                else if (u.Rank == 1)
                {
                    Application.Current.MainPage = masterAppShell;
                }
                else
                    Application.Current.MainPage = userAppShell;

            }
        }

        
        // <summary>
        public ICommand SignUpCommand { get; set; }
        private async void OnSignUp()
        {
            await Application.Current.MainPage.Navigation.PushAsync(signUpView);
        }

         // </summary>
        private bool inServerCall;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                this.inServerCall = value;
                OnPropertyChanged("NotInServerCall");
                OnPropertyChanged("InServerCall");
            }
        }

        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }
    }
}
