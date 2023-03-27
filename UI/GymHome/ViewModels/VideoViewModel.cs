using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GymHome
{
    partial class VideoViewModel : BaseViewModel
    {

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

            if (m_exerciseItems.Length == 1)
                PromptMessage = m_planEndedString;
        }



        [RelayCommand]
        public void GoBack()
        {
            if (MediaPlayerElement != null)
            {
                MediaPlayerElement.MediaPlayer.Pause();
                MediaPlayerElement = null;
            }

            try
            {
                NavigateToPreviousPage();
            }
            catch (Exception ex)
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
        private int m_currentVideoIndex = 0;
        private readonly DispatcherQueue m_dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        private const string m_planEndedString = "Acabou todos os exercicios.\nQuer terminar o plano?";
        private const string m_exerciseFinishString = "Acabou o exercicio.\nQuer fazer o proximo?";

        [ObservableProperty]
        private string title = "";

        [ObservableProperty]
        private MediaPlayerElement mediaPlayerElement;

        [ObservableProperty]
        private Visibility nextVideoPromptVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string promptMessage = m_exerciseFinishString;

        private void ResetMediaPlayerElement()
        {
            MediaPlayerElement = new MediaPlayerElement();
            MediaPlayerElement.AreTransportControlsEnabled = true;
            MediaPlayerElement.AutoPlay = true;
            MediaPlayerElement.MediaPlayer.MediaEnded += (MediaPlayer sender, object args) =>
            {
                ShowPrompt();
            };
        }

        private void ShowPrompt()
        {
            m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                () =>
                {
                    NextVideoPromptVisibility = Visibility.Visible;
                });
        }

        [RelayCommand]
        private void NextVideo(string obj = null)
        {
            Task.Run(async () => { await NextVideo(); });
        }

        [RelayCommand]
        private void ClosePrompt(string obj = null)
        {
            m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                () =>
                {
                    NextVideoPromptVisibility = Visibility.Collapsed;
                });
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

        private void InitCommands()
        {
            AddCommand(FinishPlan, Settings.VoiceKeywords.VideoPageEndPlan);
            AddCommand(Play, Settings.VoiceKeywords.VideoPlay);
            AddCommand(Pause, Settings.VoiceKeywords.VideoPause);
            AddCommand(NextVideo, Settings.VoiceKeywords.VideoNext);
            AddCommand(ClosePrompt, Settings.VoiceKeywords.Deny);
            AddCommand(NextVideo, Settings.VoiceKeywords.Confirm);
        }

        private void RemoveCommands()
        {
            RemoveCommand(Settings.VoiceKeywords.VideoPageEndPlan);
            RemoveCommand(Settings.VoiceKeywords.VideoPlay);
            RemoveCommand(Settings.VoiceKeywords.VideoPause);
            RemoveCommand(Settings.VoiceKeywords.VideoNext);
            RemoveCommand(Settings.VoiceKeywords.Deny);
            RemoveCommand(Settings.VoiceKeywords.Confirm);
        }
        private void FinishPlan(string obj = null)
        {
            Navigate(typeof(PlanPage));
        }

        private void Pause(string obj = null)
        {
            if (MediaPlayerElement != null)
                MediaPlayerElement.MediaPlayer.Pause();
        }

        private void Play(string obj = null)
        {
            if (MediaPlayerElement != null)
                MediaPlayerElement.MediaPlayer.Play();
        }

        private async Task NextVideo()
        {
            ClosePrompt();
            if (m_currentVideoIndex + 1 < m_exerciseItems.Length)
            {
                m_currentVideoIndex++;
                await LoadVideo();
                if (m_currentVideoIndex + 1 == m_exerciseItems.Length)
                    m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                    () =>
                    {
                        PromptMessage = m_planEndedString;
                    });
            }
            else
            {
                m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                    () =>
                    {
                        Navigate(typeof(PlanPage));
                    });

            }
        }

        private async Task PreviousVideo()
        {
            ClosePrompt();
            if (m_currentVideoIndex > 0)
            {
                m_currentVideoIndex--;
                await LoadVideo();
                m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                    () =>
                    {
                        PromptMessage = m_exerciseFinishString;
                    });
            }
        }

        private async Task LoadVideo()
        {
            m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                    () =>
                    {
                        Title = m_exerciseItems[m_currentVideoIndex].Title;
                        ResetMediaPlayerElement();
                    });

            await SetMediaSource(m_exerciseItems[m_currentVideoIndex].VideoID);

        }

        protected override void OnNavigatedFrom()
        {
            base.OnNavigatedFrom();
            if (MediaPlayerElement != null)
            {
                MediaPlayerElement.MediaPlayer.Pause();
                MediaPlayerElement = null;
            }
            RemoveCommands();
        }
    }
}
