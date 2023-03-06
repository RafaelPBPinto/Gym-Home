using CommunityToolkit.Mvvm.Input;
using System;

namespace GymHome
{
    partial class VideoViewModel : BaseViewModel
    {
        public string Title => m_exerciseItems == null ? string.Empty : m_exerciseItems[0].Title;

        public VideoViewModel(IExerciseItem[] exerciseItems)
        {
            if (exerciseItems == null)
            {
                Logger.Error("VideoViewModel received a null list of exercise items.");
                throw new ArgumentNullException(nameof(exerciseItems));
            }

            if (exerciseItems.Length == 0)
            {
                Logger.Error("VideoViewModel received an empty list. Can't play video.");
                throw new ArgumentException("Parameter can't be an empty list", nameof(exerciseItems));
            }

            m_exerciseItems = exerciseItems;
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }

        private IExerciseItem[] m_exerciseItems = null;
        private int currentVideoIndex = 0;

        private void PlayVideo()
        {
            throw new NotImplementedException();
        }

        private void NextVideo()
        {
            if (currentVideoIndex + 1 < m_exerciseItems.Length)
            {
                currentVideoIndex++;
                //play video
            }
        }
    }
}
