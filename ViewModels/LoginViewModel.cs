﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;

        
        public LoginViewModel()
        {

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

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
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
            this.ShowNameError = string.IsNullOrEmpty(Name);
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

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
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
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
        }
        #endregion

        //#region Email
        //private bool showEmailError;

        //public bool ShowEmailError//ShowEmailError
        //{
        //    get => showNameError;
        //    set
        //    {
        //        showNameError = value;
        //        OnPropertyChanged("ShowEmailError");
        //    }
        //}

        //private string email;

        //public string Email
        //{
        //    get => email;
        //    set
        //    {
        //        email = value;
        //        ValidateEmail();
        //        OnPropertyChanged("Email");
        //    }
        //}

        //private string emailError;

        //public string EmailError
        //{
        //    get => emailError;
        //    set
        //    {
        //        emailError = value;
        //        OnPropertyChanged("EmailError");
        //    }
        //}

        //private void ValidateEmail()
        //{
        //    string email = Email;
        //    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //    Match match = regex.Match(email);
        //    this.ShowEmailError = !match.Success;
        //}
        //#endregion

        public LoginViewModel(TriviaWebAPIProxy service) 
        {
            InServerCall = false;
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
        }

        public ICommand LoginCommand { get; set; }
        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall=true;
            //await Shell.Current.GoToAsync("connectingToServer");
            User u  = await this.triviaService.LoginAsync("ofer@ofer.com", "1234");
            //await Shell.Current.Navigation.PopModalAsync();
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                
                await Shell.Current.DisplayAlert("Login", "Login Faild!", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Login", $"Login Succeed! for {u.Name} with {u.Questions.Count} Questions", "ok");
            }
        }

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
