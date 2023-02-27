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
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GymHome
{
    /// <summary>
    /// A page that shows the exercises available to the user.
    /// </summary>
    public sealed partial class ExercisesPage : Page
    {
        public ExercisesPage()
        {
            this.InitializeComponent();
            DataContext = new ExercisesViewModel();
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var key = e.Key;

            if (key == Windows.System.VirtualKey.Down)
                ((ExercisesViewModel)DataContext).NextItem();
            else if(key == Windows.System.VirtualKey.Up)
                ((ExercisesViewModel)DataContext).PreviousItem();

            e.Handled= true;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await ((ExercisesViewModel)DataContext).PageLoaded();
        }
    }
}
