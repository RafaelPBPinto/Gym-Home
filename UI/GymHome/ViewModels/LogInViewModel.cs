using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome
{
    partial class LogInViewModel : BaseViewModel
    {
        //[ObservableProperty]
        //string email;

        //[ObservableProperty]
        //string password;

        public LogInViewModel() 
        { 
        }

        //[RelayCommand]
        //void LogIn()
        //{
        //    if (Email == null || Password == null)
        //    {
        //        Console.WriteLine("NULL");
        //        return;
        //    }

        //    // Testing purposes only
        //    // Major security flaw
        //    // Should be tested server side
        //    if (Email == "admin@admin.pt" && Password == "admin")
        //        Navigate(typeof(MainPage));
        //    else
        //        Console.WriteLine("wrong values");
        //}

        [RelayCommand]
        private void MariaLogIn()
        {
            Settings.UserID = 1;
            Navigate(typeof(MainPage));
        }
        
        [RelayCommand]
        private void AlbertoLogIn()
        {
            Settings.UserID = 2;
            Navigate(typeof(MainPage));
        }
        
        [RelayCommand]
        private void AntonioLogIn()
        {
            Settings.UserID = 3;
            Navigate(typeof(MainPage));
        }

    }
}
