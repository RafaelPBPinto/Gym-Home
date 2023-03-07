using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace GymHome
{
    public class ExerciseItem : IExerciseItem
    {
        private static int index = 0;

        public int Index { get; private set; }

        /// <summary>
        /// Gets or sets the title of the exercise
        /// </summary>
        [JsonPropertyName("nome")]
        public string Title { get; set; }

        public string TitleString => $"{Title}({Index})";

        /// <summary>
        /// Gets or sets the author of the exercise
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the duration in seconds of the exercise
        /// </summary>
        [JsonPropertyName("duracao")]
        public int Duration { get; set; }

        public string DurationString => $"Duracao: {Duration} seg";

        /// <summary>
        /// Gets or sets the description of the exercise
        /// </summary>
        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of exercise
        /// </summary>
        [JsonPropertyName("tipo")]
        public string ExerciseType { get; set; }

        [JsonPropertyName("imagem")]
        [JsonConverter(typeof(ImageDataConverter))]
        public byte[] ImageBytes { get; set; }

        public string ImageSource => m_imagePath;

        public int VideoID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private string m_imagePath;
        /// <summary>
        /// Represents an exercise
        /// </summary>
        /// <param name="title">Title of the exercise</param>
        /// <param name="author">Author of the exercise</param>
        /// <param name="duration">Duration in seconds of the exercise</param>
        /// <param name="description">Description of the exercise</param>
        /// <param name="exerciseType">Type of the exercise</param>
        public ExerciseItem(string title, string author, int duration, string description, string exerciseType, byte[] imageBytes = null)
        {
            Title = title;
            Author = author;
            Duration = duration;
            Description = description;
            ExerciseType = exerciseType;
            ImageBytes = imageBytes;
            Index = index + 1;
            m_imagePath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"),$"{TitleString}.png");
            SaveImage();

            index++;
        }

        private void SaveImage() 
        {
            File.WriteAllBytes(m_imagePath, ImageBytes);
        }

        public static void ResetIndex()
        {
            index = 0;
        }
    }

    public class ImageDataConverter : JsonConverter<byte[]>
    {
        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Convert.FromBase64String(reader.GetString());
        }
    }
}
