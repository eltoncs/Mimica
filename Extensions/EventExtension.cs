using Mimica.Entities;
using System.Collections.Concurrent;

namespace Mimica.Extensions
{
    public static class EventExtension
    {
        public static string GetCSVLine(this Event @event)
        {
            return $"{@event.TimeStamp},{@event.EventType},{@event.KeyPressed},{@event.ScreenShotPath}";
        }

        public static IEnumerable<string> GetCSVLines(
            this ConcurrentQueue<Event> eventQueue,
            bool dequeue = false)
        {
            foreach (Event ev in eventQueue)
            {
                if (ev.ScreenShotPath == null)
                {
                    continue;
                }

                if (dequeue)
                {
                    if (eventQueue.TryDequeue(out Event? dequeuedEvent))
                    {
                        yield return dequeuedEvent.GetCSVLine();
                        continue;
                    }
                }

                if (eventQueue.TryPeek(out Event? peekedEvent))
                {
                    yield return peekedEvent.GetCSVLine();
                }
            }
        }
    }
}
