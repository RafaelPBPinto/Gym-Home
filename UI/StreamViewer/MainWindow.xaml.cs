// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
            webview.Source = new Uri($"https://www.youtube.com/embed/{streamID}");
            panel.Visibility = Visibility.Collapsed;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
