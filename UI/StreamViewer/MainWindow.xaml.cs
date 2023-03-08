// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using System;
using System.Web;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace StreamViewer
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public string streamID = "";

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ButtonOkClick(object sender, RoutedEventArgs e)
        {
            if (streamID.Contains("youtube.com"))
            {
                var uri = new Uri(streamID);
                var parameters = HttpUtility.ParseQueryString(uri.Query);
                streamID = parameters["v"];
            }
            webview.Source = new Uri($"https://www.youtube.com/embed/{streamID}");
            panel.Visibility = Visibility.Collapsed;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
