using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome
{
    partial class VideoViewModel : BaseViewModel
    {
        public string Title => m_exerciseItem == null ? string.Empty : m_exerciseItem.Title;

        public VideoViewModel(ExerciseItem item) 
        {
            m_exerciseItem = item;
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }

        private ExerciseItem m_exerciseItem = null;
    }
}
