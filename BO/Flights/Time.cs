namespace BO
{
    public class Time
    {
        public Real real { get; set; }
        public Scheduled scheduled { get; set; }
        public Estimated estimated { get; set; }
        public Other other { get; set; }
        public Historical historical { get; set; }
    }
}