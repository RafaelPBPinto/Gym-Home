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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GymHome
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExercisesWindow : Window
    {
        public ExercisesWindow()
        {
            this.InitializeComponent();
            var items = CreateDummyList();

            foreach (var item in items)
                ExercisesList.Items.Add(item);
        }

        private ObservableCollection<ExerciseItem> CreateDummyList()
        {
            ObservableCollection<ExerciseItem> itemList = new ObservableCollection<ExerciseItem>();

            ExerciseItem item = new ExerciseItem("Pernas e Costas", "Criado por: Joao Manuel", "Duracao: 1h30min", "pernas e costas é bom para todos os velhotes. aqui praticamos costas principalmente mas tambem um bocadinho de pernas.");
            itemList.Add(item);

            item = new ExerciseItem("Costas", "Criado por: Jose Silva", "Duracao: 1h", "exercicio para costas. perfeito para todos os velhotes!!!");
            itemList.Add(item);

            item = new ExerciseItem("Bracos", "Criado por: Emanuel Costa", "Duracao: 45min", "exercicio de braco. bom para velhotes com dores nos bracos ou com dificuldades em mexe-los.");
            itemList.Add(item);

            item = new ExerciseItem("Bracos e Pernas", "Criado por: Tiago Amorim", "Duracao: 1h15min", "exercicio de bracos e pernas. uma mistura para quem precisa.");
            itemList.Add(item);

            item = new ExerciseItem("Bracos", "Criado por: Jose Silva", "Duracao: 26seg", "exercicio de bracos (push ups)");
            itemList.Add(item);

            return itemList;
        }
    }
}
