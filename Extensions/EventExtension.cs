using Mimica.Entities;
using System.Collections.Concurrent;

namespace Mimica.Extensions
{
    public static class EventExtension
    {
        /// <summary>
        /// Extract all lines from the event queue parsing them to CSV format.
        /// </summary>
        /// <param name="eventQueue"></param>
        /// <param name="dequeue"></param>
        /// <returns>csv enumerable</returns>
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

        /// <summary>
        /// Converts an event to a CSV line.
        /// </summary>
        /// <param name="event"></param>
        /// <returns>csv event</returns>
        public static string GetCSVLine(this Event @event)
        {
            return $"{@event.TimeStamp},{@event.EventType},{@event.KeyPressed},{@event.ScreenShotPath}";
        }
    }
}
