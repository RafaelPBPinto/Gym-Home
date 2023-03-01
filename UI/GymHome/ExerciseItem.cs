using System.Text.Json.Serialization;

namespace GymHome
{
    class ExerciseItem
    {
        /// <summary>
        /// Gets or sets the title of the exercise
        /// </summary>
        [JsonPropertyName("nome")]
        public string Title { get; set; }

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
        public string Type { get; set; }

        /// <summary>
        /// Represents an exercise
        /// </summary>
        /// <param name="title">Title of the exercise</param>
        /// <param name="author">Author of the exercise</param>
        /// <param name="duration">Duration in seconds of the exercise</param>
        /// <param name="description">Description of the exercise</param>
        /// <param name="type">Type of the exercise</param>
        public ExerciseItem(string title = "", string author = "", int duration = 0, string description = "", string type = "")
        {
            Title = title;
            Author = author;
            Duration = duration;
            Description = description;
            Type = type;
        }
    }
}
