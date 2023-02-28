using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymHome
{
    public partial class Plan : ObservableObject
    {
        [JsonPropertyName("nome")]
        public string Title { get; set; }

        [JsonPropertyName("Autor")]
        public string Author { get; set; }

        public string AuthorString => $"Autor: {Author}";

        [JsonPropertyName("dia")]
        public string Day { get; set; }

        public string DayString => $"Para fazer na {Day} feira";

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("exercicio")]
        public PlanExercise bufferPlanExercise { get; set; }

        [ObservableProperty]
        public ObservableCollection<PlanExercise> planExercise = new ObservableCollection<PlanExercise>();

        [JsonConstructor]
        public Plan(string title, string author, string day, string description, PlanExercise bufferPlanExercise)
        {
            Title = title;
            Author = author;
            Day = day;
            Description = description;
            this.bufferPlanExercise = bufferPlanExercise;
            PlanExercise.Add(bufferPlanExercise);
        }
    }

    public class PlanExercise
    {
        [JsonPropertyName("nome")]
        public string Title { get; set; }

        [JsonPropertyName("series")]
        public int Series { get; set; }

        public string SeriesString
        {
            get
            {
                return $"{Series} series";
            }
        }

        [JsonPropertyName("repeticoes")]
        public int Repetitions { get; set; }

        public string RepetitionsString => $"{Repetitions} repeticoes";

        [JsonPropertyName("tipo")]
        public string ExerciseType { get; set; }

        public string ExerciseTypeString => $"Tipo: {ExerciseType}";
    }
}
