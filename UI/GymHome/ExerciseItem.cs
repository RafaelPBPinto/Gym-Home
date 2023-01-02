using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome
{
    public class ExerciseItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Duration { get; set; }

        public string Description { get; set; }

        public ExerciseItem(string title, string author, string duration, string description)
        {
            Title = title;
            Author = author;
            Duration = duration;
            Description = description;
        }

        public ExerciseItem() { }
    }
}
