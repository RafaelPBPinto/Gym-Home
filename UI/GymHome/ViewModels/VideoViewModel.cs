using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace GymHome
{
    partial class VideoViewModel : BaseViewModel
    {
        public string Title => m_exerciseItems == null ? string.Empty : m_exerciseItems[0].Title;

        [ObservableProperty]
        public MediaPlayerElement mediaPlayer;

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
            if (!Directory.Exists("./Videos"))
                Directory.CreateDirectory("./Videos");
        }

        [RelayCommand]
        public void GoBack()
        {
            NavigateToPreviousPage();
        }

        public async Task PageLoaded()
        {
            MediaPlayer = new MediaPlayerElement();
            MediaPlayer.AreTransportControlsEnabled = true;
            MediaPlayer.AutoPlay = true;

            HttpClient client = new HttpClient();
            int videoID = m_exerciseItems[0].VideoID;
            byte[] videoBytes = null;
            
            try
            {
                videoBytes = await client.GetByteArrayAsync($"http://localhost:5000/getVideo/video_id={videoID}");
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return;
            }

            SaveVideo(videoBytes);
            MediaPlayer.Source = MediaSource.CreateFromUri(new Uri(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos"), $"{Title}.mp4")));
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

        private void SaveVideo(byte[] videoBytes)
        {
            File.WriteAllBytes(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos"), $"{Title}.mp4"), videoBytes);
        }
    }
}
