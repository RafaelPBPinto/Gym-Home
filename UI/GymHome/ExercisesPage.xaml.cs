using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ExercisesPage : Page
    {
        //public ObservableCollection<ExerciseItem> Items { get; set; }

        public ExercisesPage()
        {
            this.InitializeComponent();
            var items = CreateDummyList();

            foreach(var item in items)
            {
                Items.Items.Add(item);
            }
        }

        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(VideoPage), (ExerciseItem)Items.SelectedItem);
        }

        private ObservableCollection<ExerciseItem> CreateDummyList()
        {
            ObservableCollection<ExerciseItem> itemList = new ObservableCollection<ExerciseItem>();

            ExerciseItem item = new ExerciseItem("Pernas e Costas", "Joao Manuel", "1h30min","pernas e costas é bom para todos os velhotes. aqui praticamos costas principalmente mas tambem um bocadinho de pernas.");
            itemList.Add(item);

            item = new ExerciseItem("Costas", "Criado por: Jose Silva", "Duracao: 1h","exercicio para costas. perfeito para todos os velhotes!!!");
            itemList.Add(item);

            item = new ExerciseItem("Bracos", "Criado por: Emanuel Costa", "Duracao: 45min","exercicio de braco. bom para velhotes com dores nos bracos ou com dificuldades em mexe-los.");
            itemList.Add(item);

            item = new ExerciseItem("Bracos e Pernas", "Criado por: Tiago Amorim", "Duracao: 1h15min","exercicio de bracos e pernas. uma mistura para quem precisa.");
            itemList.Add(item);

            return itemList;
        }
    }
}
