using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome
{
    public class BaseViewModel: ObservableObject
    {
        public BaseViewModel() 
        {
            if (!KeywordExists("voltar"))
                AddCommand(NavigateToPreviousPage, "voltar");
        }

        /// <summary>
        /// Navigate to a page.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        public void Navigate(Type pageType,object param = null)
        {
            OnNavigatedFrom();
            ((App)App.Current).Navigate(pageType,param);
        }

        /// <summary>
        /// Navigate to the most recent page in the page history.
        /// </summary>
        public void NavigateToPreviousPage()
        {
            OnNavigatedFrom();
            ((App)App.Current).NavigateToPreviousPage();
        }

        public void AddCommand(Action<string> action,string keyword)
        {
            ((App)App.Current).AddCommand(action,keyword);
        }

        public void RemoveCommand(string keyword)
        {
            ((App)App.Current).RemoveCommand(keyword);
        }

        public bool KeywordExists(string keyword) 
        {
            return ((App)App.Current).keywordExists(keyword);
        }

        /// <summary>
        /// Wrapper function for voice command to call <see cref="NavigateToPreviousPage"/>
        /// </summary>
        /// <param name="obj"></param>
        private void NavigateToPreviousPage(string obj = null)
        {
            NavigateToPreviousPage();
        }

        protected virtual void OnNavigatedFrom()
        {
        }
    }
}
