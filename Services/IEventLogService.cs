using Mimica.Entities;

public interface IEventLogService
{
    Task StartLogCapture(
        Queue<Event> eventQueue,
        string user,
        int intervalMs = 2000,
        PictureBox? saveIcon = null);
}
