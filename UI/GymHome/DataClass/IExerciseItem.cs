namespace GymHome
{
    public interface IExerciseItem
    {
        public string Title { get; set; }
        public string ExerciseType { get; set; }
        public int VideoID { get; set; }
    }
}
