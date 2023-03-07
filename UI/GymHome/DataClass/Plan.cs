using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymHome
{
    public partial class Plan : ObservableObject
    {
        private static int index = 0;

        public string IndexString { get; private set; }

        [JsonPropertyName("nome")]
        public string Title { get; set; }

        public string TitleString => $"{Title}({IndexString})";

        [JsonPropertyName("Autor")]
        public string Author { get; set; }

        public string AuthorString => $"Autor: {Author}";

        [JsonPropertyName("dia")]
        public string Day { get; set; }

        public string DayString => $"Para fazer na {Day} feira";

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("exercicios")]
        public PlanExercise[] bufferPlanExercise { get; set; }

        [ObservableProperty]
        public ObservableCollection<PlanExercise> planExercise = new ObservableCollection<PlanExercise>();

        public string ImageSource => "";

        private string m_imagePath;

        [JsonConstructor]
        public Plan(string title, string author, string day, string description, PlanExercise[] bufferPlanExercise)
        {
            Title = title;
            Author = author;
            Day = day;
            Description = description;
            this.bufferPlanExercise = bufferPlanExercise;

            foreach (var exercise in bufferPlanExercise)
                PlanExercise.Add(exercise);

            IndexString = (index+1).ToString();
            m_imagePath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"), $"{TitleString}.png");

            if(!File.Exists(m_imagePath))
                SaveImage(PlanExercise[0].ImageBytes);

            index++;
        }

        public static void ResetIndex()
        {
            index = 0;
        }

        private void SaveImage(byte[] imageBytes)
        {
            if (imageBytes == null)
                return;
            File.WriteAllBytes(m_imagePath, imageBytes);
        }
    }

    public class PlanExercise : IExerciseItem
    {
        [JsonPropertyName("nome")]
        public string Title { get; set; }

        [JsonPropertyName("series")]
        public int Series { get; set; }

        public string SeriesString => $"{Series} series";

        [JsonPropertyName("repeticoes")]
        public int Repetitions { get; set; }

        public string RepetitionsString => $"{Repetitions} repeticoes";

        [JsonPropertyName("tipo")]
        public string ExerciseType { get; set; }

        public string ExerciseTypeString => $"Tipo: {ExerciseType}";

        [JsonPropertyName("videoID")]
        public int VideoID { get; set; }

        [JsonPropertyName("imagem")]
        [JsonConverter(typeof(ImageDataConverter))]
        public byte[] ImageBytes { get; set; }
    }
}
