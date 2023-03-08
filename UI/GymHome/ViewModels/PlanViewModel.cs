using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GymHome
{
    public partial class PlanViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Title))]
        [NotifyPropertyChangedFor(nameof(Description))]
        private int selectedIndex = 0;

        [ObservableProperty]
        private int pageNumber = 0;

        [ObservableProperty]
        ObservableCollection<Plan> plans = new ObservableCollection<Plan>();

        public string Title => Plans.Count == 0 ? string.Empty : Plans[SelectedIndex].Title;

        public string Description => Plans.Count == 0 ? string.Empty : Plans[SelectedIndex].Description;

        public PlanViewModel()
        {
            InitCommands();
            Plan.ResetIndex();
        }

        private Expander m_lastOpenExpander = null;
        private List<Plan> m_allPlans;
        private readonly int m_elementsPerPage = 10;

        public void Expander_Expanding(Expander expander)
        {
            if (m_lastOpenExpander == null)
                m_lastOpenExpander = expander;

            if (m_lastOpenExpander != expander)
            {
                m_lastOpenExpander.IsExpanded = false;
                m_lastOpenExpander = expander;
            }

            var index = int.Parse(((TextBlock)((Grid)expander.Header).Children[0]).Text);
            SelectedIndex = index - 1;
        }

        public async Task PageLoaded()
        {
            HttpClient client = new HttpClient();

            try
            {
                m_allPlans = await client.GetFromJsonAsync<List<Plan>>($"http://localhost:5000/profileComplete/user_id={Settings.UserID}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }

            for (int i = 0; i < m_allPlans.Count && i < m_elementsPerPage; i++)
            {
                Plans.Add(m_allPlans[i]);
            }


            SelectedIndex = 0;
            PageNumber = 1;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }

        [RelayCommand]
        public void StartPlan()
        {
            if (Plans.Count == 0)
                return;

            Plan plan = Plans[SelectedIndex];
            Navigate(typeof(VideoPage), plan.PlanExercise.ToArray());
        }

        [RelayCommand]
        private void NextListPage()
        {
            if (m_allPlans.Count / m_elementsPerPage < PageNumber - 1)
                return;

            Plans.Clear();

            int startPoint = PageNumber * m_elementsPerPage;
            for (int i = startPoint; i < m_allPlans.Count && i < startPoint + m_elementsPerPage; i++)
            {
                Plans.Add(m_allPlans[i]);
            }

            SelectedIndex = 0;
            PageNumber++;
        }

        [RelayCommand]
        private void PreviousListPage()
        {
            if (PageNumber == 1)
                return;

            Plans.Clear();
            int startPoint = (PageNumber - 2) * m_elementsPerPage;
            for (int i = startPoint; i < m_allPlans.Count && i < startPoint + m_elementsPerPage; i++)
            {
                Plans.Add(m_allPlans[i]);
            }

            SelectedIndex = 0;
            PageNumber--;
        }
        private void NextListPage(string obj = null)
        {
            NextListPage();
        }

        private void PreviousListPage(string obj = null)
        {
            PreviousListPage();
        }

        private void StartPlan(string obj = null)
        {
            StartPlan();
        }

        private readonly Dictionary<string, int> m_stringNumToInt = new Dictionary<string, int>
        {
            {"um",1},
            {"dois",2 },
            {"tres",3 },
            {"quatro",4 },
            {"cinco",5 },
            {"seis",6 },
            {"sete",7 },
            {"oito",8 },
            {"nove",9 },
            {"dez",10 }
        };

        private void SelectPlan(string obj = null)
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
            AddCommand(SelectPlan, Settings.VoiceKeywords.PlanPageSelectPlan);
            AddCommand(NextListPage, Settings.VoiceKeywords.ExercisesPageNextListPage);
            AddCommand(PreviousListPage, Settings.VoiceKeywords.ExercisesPagePreviousListPage);
            AddCommand(StartPlan, Settings.VoiceKeywords.ExercisesPageStartExercise);
        }

        protected override void OnNavigatedFrom()
        {
            RemoveCommand(Settings.VoiceKeywords.PlanPageSelectPlan);
            AddCommand(NextListPage, Settings.VoiceKeywords.ExercisesPageNextListPage);
            AddCommand(PreviousListPage, Settings.VoiceKeywords.ExercisesPagePreviousListPage);
            AddCommand(StartPlan, Settings.VoiceKeywords.ExercisesPageStartExercise);
        }
    }
}
