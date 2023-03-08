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
        int selectedIndex = 0;

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
            List<Plan> m_plans = null;

            try
            {
                m_plans = await client.GetFromJsonAsync<List<Plan>>($"http://localhost:5000/profileComplete/user_id={Settings.UserID}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }

            foreach (Plan plan in m_plans)
            {
                Plans.Add(plan);
            }

            SelectedIndex = 0;
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

        private void SelectPlan(string obj = null)
        {
            if (obj == null)
                return;

            int index = 0;

            try
            {
                index = int.Parse(obj);
            }
            catch
            {
                Debug.WriteLine($"invalid index: {obj}");
                return;
            }

            index--;
            if (index < 0 || index > Plans.Count - 1)
                return;

            SelectedIndex = index;
        }

        private void InitCommands()
        {
            AddCommand(SelectPlan, Settings.VoiceKeywords.PlanPageSelectPlan);
        }

        protected override void OnNavigatedFrom()
        {
            RemoveCommand(Settings.VoiceKeywords.PlanPageSelectPlan);
        }
    }
}
