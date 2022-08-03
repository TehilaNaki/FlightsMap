namespace BO
{
    public class Position
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int altitude { get; set; }
        public Country country { get; set; }
        public Region region { get; set; }
    }
}