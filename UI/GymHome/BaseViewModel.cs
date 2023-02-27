using CommunityToolkit.Mvvm.ComponentModel;
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
        }

        /// <summary>
        /// Navigate to a page.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        public void Navigate(Type pageType)
        {
            ((App)App.Current).Navigate(pageType);
        }

        /// <summary>
        /// Navigate to the most recent page in the page history.
        /// </summary>
        public void NavigateToPreviousPage()
        {
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
    }
}
