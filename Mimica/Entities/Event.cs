namespace Mimica.Entities
{
    public class Event
    {
        public long TimeStamp { get; set; }
        public EventType EventType { get; set; }
        public string? KeyPressed { get; set; }
        public Bitmap? screenShotImg { get; set; }
        public string? ScreenShotPath { get; set; }
    }
}
