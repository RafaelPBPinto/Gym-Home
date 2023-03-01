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
using System.Text;
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

        public string Title
        {
            get
            {
                if(Plans.Count > 0)
                    return Plans[SelectedIndex].Title;

                return string.Empty;
            }
        }

        public string Description
        {
            get
            {
                if(Plans.Count > 0)
                    return Plans[SelectedIndex].Description;

                return string.Empty;
            }
        }

        public PlanViewModel() 
        {
            AddCommand(SelectPlan, "selecionar_opcao");
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
            SelectedIndex = index-1;
        }

        public async Task PageLoaded(int userID)
        {
            HttpClient client = new HttpClient();
            List<Plan> m_plans = null;

            try
            {
                m_plans = await client.GetFromJsonAsync<List<Plan>>($"http://localhost:5000/profileComplete/user_id={userID}");
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
            if(index < 0 || index > Plans.Count - 1)
                return;

            SelectedIndex = index;
        }

        protected override void OnNavigatedFrom()
        {
            Plan.ResetIndex();
            RemoveCommand("selecionar_opcao");
        }
    }
}
