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
        private enum ExerciseType
        {
            Single,
            Multiple
        };

        public string Title => m_exerciseItem == null ? string.Empty : m_exerciseItem.Title;

        public VideoViewModel(ExerciseItem item) 
        {
            m_exerciseItem = item;
            m_exerciseType = ExerciseType.Single;
        }

        public VideoViewModel(PlanExercise[] plan)
        {
            m_plan = plan;
            m_exerciseType = ExerciseType.Multiple;
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }

        //TODO: make PlanExercise inherit ExerciseItem
        private ExerciseItem m_exerciseItem = null;
        private PlanExercise[] m_plan = null;
        private ExerciseType m_exerciseType;
        private int currentVideoIndex = 0;

        private void PlayVideo()
        {
            if(m_exerciseType == ExerciseType.Single) 
            {
                //play video with link in m_exerciseItem
            }
            else if(m_exerciseType == ExerciseType.Multiple)
            {
                //Play video with link in m_plan[0]
            }
        }

        private void NextVideo()
        {
            if(currentVideoIndex + 1 < m_plan.Length) 
            {
                currentVideoIndex++;
                //play video
            }
        }
    }
}
