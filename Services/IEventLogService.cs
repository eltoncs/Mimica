using Mimica.Entities;
using System.Collections.Concurrent;

public interface IEventLogService
{
    Task StartLogCapture(
        ConcurrentQueue<Event> eventQueue,
        string user,
        int intervalMs = 2000,
        PictureBox? saveIcon = null);
}
