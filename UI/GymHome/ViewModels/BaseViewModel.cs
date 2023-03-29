using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;

namespace GymHome
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private Uri microfoneImageSource;

        [ObservableProperty]
        private bool isOpen = false;

        [ObservableProperty]
        private string message = "";

        protected static readonly Settings settingsInstance = Settings.Instance;

        public BaseViewModel()
        {
            if (!KeywordExists(settingsInstance.voiceKeywords.NavigateToPreviousPage))
                AddCommand(NavigateToPreviousPage, settingsInstance.voiceKeywords.NavigateToPreviousPage);

            if(!KeywordExists(settingsInstance.voiceKeywords.NavigateToMainPage))
                AddCommand(NavigateToMainPage,settingsInstance.voiceKeywords.NavigateToMainPage);

            //no if check because although it will exist, it is using the imageSource variable previous declared
            //meaning it will only work in the first page
            //this way it will override the variable used in the methods and work properly
            AddCommand(NotListening, settingsInstance.voiceKeywords.MicrofoneMute);
            AddCommand(Listening, settingsInstance.voiceKeywords.MicrofoneUnmute);
            AddCommand(ShowLegend, settingsInstance.voiceKeywords.MicrofoneMessageCaught);
            ((App)App.Current).OnAnyMessageReceived += HideLegend;
            NotListening();
        }

        /// <summary>
        /// Navigate to a page.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        protected void Navigate(Type pageType, object param = null)
        {
            OnNavigatedFrom();
            ((App)App.Current).OnAnyMessageReceived -= HideLegend;
            ((App)App.Current).Navigate(pageType, param);
        }

        /// <summary>
        /// Navigate to the most recent page in the page history.
        /// </summary>
        protected void NavigateToPreviousPage()
        {
            OnNavigatedFrom();
            ((App)App.Current).NavigateToPreviousPage();
        }

        protected void AddCommand(Action<string> action, string keyword)
        {
            ((App)App.Current).AddCommand(action, keyword);
        }

        protected void RemoveCommand(string keyword)
        {
            ((App)App.Current).RemoveCommand(keyword);
        }

        protected bool KeywordExists(string keyword)
        {
            return ((App)App.Current).keywordExists(keyword);
        }

        protected void NavigateToMainPage(string obj = null)
        {
            Navigate(typeof(MainPage));
        }

        protected virtual void OnNavigatedFrom()
        {
        }

        
        private static readonly string m_baseImagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");

        /// <summary>
        /// Wrapper function for voice command to call <see cref="NavigateToPreviousPage"/>
        /// </summary>
        /// <param name="obj"></param>
        private void NavigateToPreviousPage(string obj = null)
        {
            NavigateToPreviousPage();
        }

        private void Listening(string obj = null)
        {
            string path = Path.Combine(m_baseImagesPath, "mic_unmuted.png");
            if (!File.Exists(path))
            {
                Logger.Error($"Couldn't find image {path}");
                return;
            }
            MicrofoneImageSource = new Uri(path);
        }

        private void NotListening(string obj = null)
        {
            string path = Path.Combine(m_baseImagesPath, "mic_muted.png");
            if (!File.Exists(path))
            {
                Logger.Error($"Couldn't find image {path}");
                return;
            }
            MicrofoneImageSource = new Uri(path);
        }

        private void ShowLegend(string obj = null)
        {
            if (obj == null)
                return;

            Message = obj;
            IsOpen = true;
        }

        private void HideLegend(string obj = null)
        {
            IsOpen = false;
            Message = string.Empty;
        }
    }
}
