using System.Text.Json.Serialization;

namespace GymHome
{
    public interface IExerciseItem
    {
        public string Title { get; set; }
        public string ExerciseType { get; set; }
    }
}
