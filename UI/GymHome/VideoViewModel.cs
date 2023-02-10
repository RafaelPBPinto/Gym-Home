using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome
{
    partial class VideoViewModel : BaseViewModel
    {
        public VideoViewModel() 
        {
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }
    
    }
}
