namespace BO
{
    public class Status
    {
        public bool live { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public object estimated { get; set; }
        public bool ambiguous { get; set; }
        public Generic generic { get; set; }
        public string color { get; set; }
        public string type { get; set; }
    }
}