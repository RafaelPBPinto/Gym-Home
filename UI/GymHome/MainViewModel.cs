using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome
{
    partial class MainViewModel : BaseViewModel
    {
        public MainViewModel() 
        {
            
        }

        /// <summary>
        /// Navigate to the exercises page.
        /// Shows all exercises available to the user.
        /// </summary>
        [RelayCommand]
        public void ListAllExercises()
        {
            Navigate(typeof(ExercisesPage));
        }
    }
}
