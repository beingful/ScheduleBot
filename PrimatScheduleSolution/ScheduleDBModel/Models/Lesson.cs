using System.Collections.Generic;

namespace DataBaseExploitation.Models
{
    public class Lesson
    {
        public string Description { get; set; }
        public string LessonNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Subject { get; set; }
        public List<string> Professor { get; set; }
        public string Type { get; set; }
        public string Fraction { get; set; }
        public string Link { get; set; }

        public Lesson() => Professor = new List<string>();
    }
}
