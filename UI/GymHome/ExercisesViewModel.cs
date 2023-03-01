using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GymHome
{
    partial class ExercisesViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Title))]
        [NotifyPropertyChangedFor(nameof(Description))]
        int selectedIndex;

        [ObservableProperty]
        ObservableCollection<ExerciseItem> exerciseItems = new ObservableCollection<ExerciseItem>();
        
        public string Title
        {
            get
            {
                if(ExerciseItems.Count > 0)
                    return ExerciseItems[SelectedIndex].Title;
                else
                    return string.Empty;
            }
        }


        public string Description 
        {
            get
            {
                if(ExerciseItems.Count > 0)
                    return ExerciseItems[SelectedIndex].Description;
                else
                    return string.Empty;
            }
            
        }

        public ExercisesViewModel()
        {
            AddCommand(StartExercise, "comecar");
            AddCommand(NextItem, "proximo");
            AddCommand(PreviousItem, "anterior");
        }

        public async Task PageLoaded()
        {
            HttpClient client = new HttpClient();
            List<ExerciseItem> items = null;
            try
            {
                items = await client.GetFromJsonAsync<List<ExerciseItem>>("http://localhost:5000/getExercises");
            }
            catch 
            {
                return;
            }

            foreach (ExerciseItem item in items)
            {
                ExerciseItems.Add(item);
            }

            SelectedIndex = 0;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
        }

        [RelayCommand]
        public void StartExercise()
        {
            if (ExerciseItems.Count > 0)
                Navigate(typeof(VideoPage), ExerciseItems[SelectedIndex]);
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }

        /// <summary>
        /// Changes the selected item to be the next one in the list.
        /// </summary>
        public void NextItem()
        {
            if (ExerciseItems.Count > 0 && SelectedIndex < (ExerciseItems.Count - 1))
                SelectedIndex++;
        }

        /// <summary>
        /// Changes the selected item to be the previous one in the list.
        /// </summary>
        public void PreviousItem() 
        {
            if (ExerciseItems.Count > 0 && SelectedIndex > 0) 
                SelectedIndex--;
        }

        /// <summary>
        /// Wrapper function for voice command to call <see cref="StartExercise"/>
        /// </summary>
        /// <param name="obj"></param>
        private void StartExercise(string obj = null)
        {
            StartExercise();
        }

        /// <summary>
        /// Wrapper function for voice command to call <see cref="NextItem"/>
        /// </summary>
        /// <param name="obj"></param>
        private void NextItem(string obj = null) 
        {
            NextItem();
        }

        /// <summary>
        /// Wrapper function for voice command to call <see cref="PreviousItem"/>
        /// </summary>
        /// <param name="obj"></param>
        private void PreviousItem(string obj = null) 
        {
            PreviousItem();
        }

        protected override void OnNavigatedFrom()
        {
            RemoveCommand("comecar");
            RemoveCommand("proximo");
            RemoveCommand("anterior");
        }
    }
}
