// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.UI.Core;
using System.Windows.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GymHome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var key = e.Key;
            if (key == Windows.System.VirtualKey.Y)
            {
                if (micImage.Source.ToString().Contains("mic_unmuted.png"))
                {
                    micImage.Source = new BitmapImage(new Uri("../Assets/mic_muted.png", UriKind.Relative));
                }
                else
                {
                    micImage.Source = new BitmapImage(new Uri("../Assets/mic_unmuted.png", UriKind.Relative));
                }
            }
        }

        //private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        //{

        //    var x = e.Key;
        //    //if (x == Windows.System.VirtualKey.X)
        //    //{
        //    Debug.WriteLine("J");
        //    ((MainViewModel)DataContext).mute();
        //    //}
        //    e.Handled = true;
        //}
    }
}
