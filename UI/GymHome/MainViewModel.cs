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
            AddCommand(ListAllExercises, "todos os exercicios");
            
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

        /// <summary>
        /// Wrapper function for voice command to call <see cref="ListAllExercises"/>
        /// </summary>
        /// <param name="obj">not required. ignored</param>
        private void ListAllExercises(string obj = null)
        {
            ListAllExercises();
        }

        protected override void RemoveCommands()
        {
            RemoveCommand("todos os exercicios");
        }
    }
}
