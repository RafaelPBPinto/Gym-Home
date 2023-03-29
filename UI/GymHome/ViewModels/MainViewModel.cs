using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI.Core;

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
            AddCommand(SelectOption, settingsInstance.voiceKeywords.MainPageSelectOption);
        }

        protected override void OnNavigatedFrom()
        {
            RemoveCommand(settingsInstance.voiceKeywords.MainPageSelectOption);
        }
    }
}
