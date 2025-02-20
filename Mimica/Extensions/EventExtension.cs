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
            this ConcurrentQueue<Event> eventQueue)
        {
            var poisonQueue = new ConcurrentQueue<Event>();

            while (eventQueue.Count > 0)
            {
                if (eventQueue.TryDequeue(out Event? dequeuedEvent))
                {
                    if (dequeuedEvent.ScreenShotPath == null)
                    {
                        poisonQueue.Enqueue(dequeuedEvent);
                        continue;
                    }

                    yield return dequeuedEvent.GetCSVLine();
                }
                else
                {
                    Thread.Sleep(50);
                }
            }

            RequeuePoisonQueue(eventQueue, poisonQueue);
        }

        private static void RequeuePoisonQueue(
            ConcurrentQueue<Event> eventQueue,
            ConcurrentQueue<Event> poisonQueue)
        {
            while (poisonQueue.Count > 0)
            {
                if (poisonQueue.TryDequeue(out Event? dequeuedEvent))
                {
                    eventQueue.Enqueue(dequeuedEvent);
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
