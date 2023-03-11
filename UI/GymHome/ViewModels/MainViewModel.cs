using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace GymHome
{
    partial class MainViewModel : BaseViewModel
    {
        public MainViewModel() 
        {
            InitCommands();
        }

        public MainViewModel(int id)
        {
            InitCommands();
        }

        /// <summary>
        /// Navigate to the exercises page.
        /// Shows all exercises available to the user.
        /// </summary>
        [RelayCommand]
        void ListAllExercises()
        {
            Navigate(typeof(ExercisesPage));
        }

        [RelayCommand]
        void ShowPlans()
        {
            Navigate(typeof(PlanPage));
        }

        private void SelectOption(string obj = null)
        {
            if (obj == null)
                return;

            if (obj == "todos_exercicios")
                ListAllExercises();
            else if (obj == "planos_exercicios")
                ShowPlans();
        }


        private void InitCommands()
        {
            AddCommand(SelectOption, Settings.VoiceKeywords.MainPageSelectOption);
        }

        protected override void OnNavigatedFrom()
        {
            RemoveCommand(Settings.VoiceKeywords.MainPageSelectOption);
        }

        [ObservableProperty]
        private Visibility v = Visibility.Collapsed;
        public void mute()
        {
            Debug.WriteLine("T");
            V = Visibility.Visible;
        }
    }
}
