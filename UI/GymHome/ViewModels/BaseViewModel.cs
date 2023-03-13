using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;

namespace GymHome
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private Uri microfoneImageSource;

        private static readonly string baseImagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Assets");

        public BaseViewModel()
        {
            if (!KeywordExists(Settings.VoiceKeywords.NavigateToPreviousPage))
                AddCommand(NavigateToPreviousPage, Settings.VoiceKeywords.NavigateToPreviousPage);

            if(!KeywordExists(Settings.VoiceKeywords.NavigateToMainPage))
                AddCommand(NavigateToMainPage,Settings.VoiceKeywords.NavigateToMainPage);

            if(!KeywordExists(Settings.VoiceKeywords.MicrofoneMute))
                AddCommand(NotListening,Settings.VoiceKeywords.MicrofoneMute);

            if (!KeywordExists(Settings.VoiceKeywords.MicrofoneUnmute))
                AddCommand(Listening, Settings.VoiceKeywords.MicrofoneUnmute);

            MicrofoneImageSource = new Uri(Path.Combine(baseImagesPath, "mic_muted.png"));
        }

        /// <summary>
        /// Navigate to a page.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        protected void Navigate(Type pageType, object param = null)
        {
            OnNavigatedFrom();
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

        protected virtual void OnNavigatedFrom()
        {
        }

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
            MicrofoneImageSource = new Uri(Path.Combine(baseImagesPath, "mic_unmuted.png"));
        }

        private void NotListening(string obj = null)
        {
            MicrofoneImageSource = new Uri(Path.Combine(baseImagesPath, "mic_muted.png"));
        }

        protected void NavigateToMainPage(string obj = null)
        {
            Navigate(typeof(MainPage));
        }
        
    }
}
