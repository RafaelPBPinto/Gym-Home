using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            
        }

        public async Task PageLoaded(int id)
        {
            HttpClient client = new HttpClient();
            List<Plan> m_plans = null;

            try
            {
                m_plans = await client.GetFromJsonAsync<List<Plan>>($"http://localhost:5000/profileComplete/user_id={id}");
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

            SelectedIndex= 0;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }
    }
}
