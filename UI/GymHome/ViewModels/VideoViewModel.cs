using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;

namespace GymHome
{
    partial class VideoViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public MediaPlayerElement mediaPlayerElement;

        public VideoViewModel(IExerciseItem[] exerciseItems)
        {
            InitCommands();
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
            Title = m_exerciseItems[0].Title;
            if (!Directory.Exists("./Videos"))
                Directory.CreateDirectory("./Videos");
        }

        private void InitCommands()
        {
            AddCommand(FinishPlan, Settings.VoiceKeywords.VideoPageEndPlan);
        }

        private void FinishPlan(string obj)
        {
            if (MediaPlayerElement != null)
            {
                MediaPlayerElement.MediaPlayer.Pause();
                MediaPlayerElement = null;
            }

            NavigateToMainPage();
        }

        [RelayCommand]
        public void GoBack()
        {
            if(MediaPlayerElement != null)
            {
                MediaPlayerElement.MediaPlayer.Pause();
                MediaPlayerElement = null;
            }

            try
            {
                NavigateToPreviousPage();
            }
            catch(Exception ex) 
            {
                Logger.Error($"error while leaving video page. {ex.Message}");
            }
        }

        public async Task PageLoadedAsync()
        {
            ResetMediaPlayerElement();
            await SetMediaSource(m_exerciseItems[0].VideoID);
        }

        private IExerciseItem[] m_exerciseItems = null;
        private int currentVideoIndex = 0;
        private readonly DispatcherQueue m_dispatcherQueue = DispatcherQueue.GetForCurrentThread();

        private async void NextVideo(MediaPlayer sender, object args)
        {
            if (currentVideoIndex + 1 < m_exerciseItems.Length)
            {
                m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                    () =>
                    {
                        Title = m_exerciseItems[(currentVideoIndex + 1)].Title;
                        ResetMediaPlayerElement();
                    });

                await SetMediaSource(m_exerciseItems[(currentVideoIndex + 1)].VideoID);   
                currentVideoIndex++;
            }
        }

        private void ResetMediaPlayerElement()
        {
            MediaPlayerElement = new MediaPlayerElement();
            MediaPlayerElement.AreTransportControlsEnabled = true;
            MediaPlayerElement.AutoPlay = true;
            MediaPlayerElement.MediaPlayer.MediaEnded += NextVideo;
        }

        private async Task SetMediaSource(int videoID)
        {
            HttpClient client = new HttpClient();
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
            if (MediaPlayerElement != null)
                m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                    () =>
                    {
                        MediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos"), $"{Title}.mp4")));
                    });
        }

        private void SaveVideo(byte[] videoBytes)
        {
            File.WriteAllBytes(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos"), $"{Title}.mp4"), videoBytes);
        }

        private void RemoveCommands()
        {
            RemoveCommand(Settings.VoiceKeywords.VideoPageEndPlan);
        }

        protected override void OnNavigatedFrom()
        {
            base.OnNavigatedFrom();
            RemoveCommands();
        }
    }
}
