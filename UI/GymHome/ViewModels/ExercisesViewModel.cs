using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
        public string Title => ExerciseItems.Count == 0 ? string.Empty : ExerciseItems[SelectedIndex].Title;

        public string Description => ExerciseItems.Count == 0 ? string.Empty : ExerciseItems[SelectedIndex].Description;

        public ExercisesViewModel()
        {
            InitCommands();
            ExerciseItem.ResetIndex();
        }
        public async Task PageLoaded()
        {
            HttpClient client = new HttpClient();
            try
            {
                if (!Directory.Exists("./Images"))
                    Directory.CreateDirectory("./Images");
                allItems = await client.GetFromJsonAsync<List<ExerciseItem>>("http://localhost:5000/getExercises");
                
            }
            catch(Exception ex)
            {
                Logger.Error($"Couldn't get exercises. Reason: {ex.Message}");
                return;
            }

            if(allItems == null)
            {
                Logger.Error($"Exercises list is a null list");
                return;
            }

            for (int i = 0; i < allItems.Count && i < m_elementsPerPage; i++)
            {
                ExerciseItems.Add(allItems[i]);
            }
            
            SelectedIndex = 0;
            PageNumber = 1;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
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


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Title))]
        [NotifyPropertyChangedFor(nameof(Description))]
        private int selectedIndex;

        [ObservableProperty]
        ObservableCollection<ExerciseItem> exerciseItems = new ObservableCollection<ExerciseItem>();

        [ObservableProperty]
        private int pageNumber;
        private List<ExerciseItem> allItems = null;

        private const int m_elementsPerPage = 10;

        [RelayCommand]
        private void StartExercise()
        {
            if (ExerciseItems.Count > 0)
                Navigate(typeof(VideoPage), new IExerciseItem[1] { ExerciseItems[SelectedIndex] });
        }

        [RelayCommand]
        private void GoBack()
        {
            NavigateToPreviousPage();
        }

        private void NextListPage(string obj = null)
        {
            NextListPage();
        }

        private void PreviousListPage(string obj = null)
        {
            PreviousListPage();
        }

        [RelayCommand]
        private void NextListPage()
        {
            if (allItems.Count / m_elementsPerPage < PageNumber - 1)
                return;

            ExerciseItems.Clear();

            int startPoint = PageNumber * m_elementsPerPage;
            for (int i = startPoint; i < allItems.Count && i < startPoint + m_elementsPerPage; i++)
            {
                ExerciseItems.Add(allItems[i]);
            }

            SelectedIndex = 0;
            PageNumber++;
        }

        [RelayCommand]
        private void PreviousListPage()
        {
            if(PageNumber == 1)
                return;

            ExerciseItems.Clear();
            int startPoint = (PageNumber - 2) * m_elementsPerPage;
            for (int i = startPoint; i < allItems.Count && i < startPoint + m_elementsPerPage; i++)
            {
                ExerciseItems.Add(allItems[i]);
            }

            SelectedIndex = 0;
            PageNumber--;
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

        private readonly Dictionary<string, int> m_stringNumToInt = new Dictionary<string, int>
        {
            {"um",1},
            {"dois",2 },
            {"três",3 },
            {"quatro",4 },
            {"cinco",5 },
            {"seis",6 },
            {"sete",7 },
            {"oito",8 },
            {"nove",9 },
            {"dez",10 }
        };

        private void SelectExercise(string obj = null)
        {
            if (obj == null)
                return;

            if (!m_stringNumToInt.ContainsKey(obj))
            {
                Logger.Error($"Unknown number {obj}");
                return;
            }

            int index = m_stringNumToInt[obj];
            index--;
            SelectedIndex = index;
        }

        private void InitCommands()
        {
            //TODO: add commands and keywords to a variable like in app.xaml.cs and call the AddCommand in a loop
            //same for RemoveCommand
            AddCommand(StartExercise,Settings.VoiceKeywords.ExercisesPageStartExercise);
            AddCommand(NextItem,Settings.VoiceKeywords.ExercisesPageNextItem);
            AddCommand(PreviousItem,Settings.VoiceKeywords.ExercisesPagePreviousItem);
            AddCommand(SelectExercise,Settings.VoiceKeywords.ExercisesPageSelectExercise);
            AddCommand(NextListPage,Settings.VoiceKeywords.ExercisesPageNextListPage);
            AddCommand(PreviousListPage, Settings.VoiceKeywords.ExercisesPagePreviousListPage);
        }

        protected override void OnNavigatedFrom()
        {
            RemoveCommand(Settings.VoiceKeywords.ExercisesPageStartExercise);
            RemoveCommand(Settings.VoiceKeywords.ExercisesPageNextItem);
            RemoveCommand(Settings.VoiceKeywords.ExercisesPagePreviousItem);
            RemoveCommand(Settings.VoiceKeywords.ExercisesPageSelectExercise);
            RemoveCommand(Settings.VoiceKeywords.ExercisesPageNextListPage);
            RemoveCommand(Settings.VoiceKeywords.ExercisesPagePreviousListPage);
        }
    }
}
