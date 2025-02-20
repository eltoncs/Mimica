using Mimica.Entities;
using System.Collections.Concurrent;

public interface ICSVEventLogService
{
    Task StartLogRecording(
        ConcurrentQueue<Event> eventQueue,
        string user,
        int intervalMs = 2000,
        PictureBox? saveIcon = null);
}
