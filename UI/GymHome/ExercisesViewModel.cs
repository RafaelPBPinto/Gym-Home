using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome;

partial class ExercisesViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    [NotifyPropertyChangedFor(nameof(Description))]
    int selectedIndex;

    [ObservableProperty]
    ObservableCollection<ExerciseItem> exerciseItems = new ObservableCollection<ExerciseItem>();

    public string Title => ExerciseItems[SelectedIndex].Title;


    public string Description => ExerciseItems[SelectedIndex].Description;

    public ExercisesViewModel()
    {
        var items = GenerateDummyList();

        foreach(ExerciseItem item in items) 
        {
            ExerciseItems.Add(item);
        }
    }

    List<ExerciseItem> GenerateDummyList()
    {
        List<ExerciseItem> exerciseItems = new List<ExerciseItem>();

        ExerciseItem item = new ExerciseItem("Pernas e Costas", "Criado por: Joao Manuel", "Duracao: 1h30min", "pernas e costas é bom para todos os velhotes. aqui praticamos costas principalmente mas tambem um bocadinho de pernas.");
        exerciseItems.Add(item);

        item = new ExerciseItem("Costas", "Criado por: Jose Silva", "Duracao: 1h", "exercicio para costas. perfeito para todos os velhotes!!!");
        exerciseItems.Add(item);

        item = new ExerciseItem("Bracos", "Criado por: Emanuel Costa", "Duracao: 45min", "exercicio de braco. bom para velhotes com dores nos bracos ou com dificuldades em mexe-los.");
        exerciseItems.Add(item);

        item = new ExerciseItem("Bracos e Pernas", "Criado por: Tiago Amorim", "Duracao: 1h15min", "exercicio de bracos e pernas. uma mistura para quem precisa.");
        exerciseItems.Add(item);

        item = new ExerciseItem("Bracos", "Criado por: Jose Silva", "Duracao: 26seg", "exercicio de bracos (push ups)");
        exerciseItems.Add(item);

        return exerciseItems;
    }
}
