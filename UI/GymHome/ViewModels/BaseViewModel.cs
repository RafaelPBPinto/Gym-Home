using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace GymHome
{
    public class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
            if (!KeywordExists(Settings.VoiceKeywords.NavigateToPreviousPage))
                AddCommand(NavigateToPreviousPage, Settings.VoiceKeywords.NavigateToPreviousPage);

            if(!KeywordExists(Settings.VoiceKeywords.NavigateToMainPage))
                AddCommand(NavigateToMainPage,Settings.VoiceKeywords.NavigateToMainPage);
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

        protected void NavigateToMainPage(string obj = null)
        {
            Navigate(typeof(MainPage));
        }
        
    }
}
