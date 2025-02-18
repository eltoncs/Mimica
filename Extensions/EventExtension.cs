using Mimica.Entities;

namespace Mimica.Extensions
{
    public static class EventExtension
    {
        public static string GetCSVLine(this Event @event)
        {
            return $"{@event.TimeStamp},{@event.EventType},{@event.KeyPressed},{@event.ScreenShotPath}";
        }

        public static IEnumerable<string> GetCSVLines(
            this Queue<Event> eventQueue,
            bool dequeue = false)
        {
            while (eventQueue.Count > 0)
            {
                Event ev = dequeue ? eventQueue.Dequeue() : eventQueue.Peek();
                yield return ev.GetCSVLine();
            }
        }
    }
}
