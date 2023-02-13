using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHome
{
    class ExerciseItem
    {
        /// <summary>
        /// Gets or sets the title of the exercise
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the exercise
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the duration in seconds of the exercise
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the description of the exercise
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Represents an exercise
        /// </summary>
        /// <param name="title">Title of the exercise</param>
        /// <param name="author">Author of the exercise</param>
        /// <param name="duration">Duration in seconds of the exercise</param>
        /// <param name="description">description of the exercise</param>
        public ExerciseItem(string title = "", string author = "", string duration = "", string description = "")
        {
            Title = title;
            Author = author;
            Duration = duration;
            Description = description;
        }
    }
}
