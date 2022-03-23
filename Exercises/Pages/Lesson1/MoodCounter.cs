namespace Exercises.Pages.Lesson1
{
    public class MoodCounter
    {
        public int Happy { get; set; }
        public int Disappointed { get; set; }
        public int Angry { get; set; }

        public MoodCounter()
        {
            Happy = 0;
            Disappointed = 0;
            Angry = 0;
        }
    }
}
