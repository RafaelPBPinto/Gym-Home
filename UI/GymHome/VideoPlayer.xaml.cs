using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GymHome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPlayer : Page
    {
        public VideoPlayer()
        {
            this.InitializeComponent();
            _mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/push_up.mp4"));
            var mediaPlayer = _mediaPlayerElement.MediaPlayer;
            mediaPlayer.Play();
        }

        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    var item = (ExerciseItem)e.Parameter;
        //    _titleTexBlock.Text = item.Title;
        //}
    }
}
